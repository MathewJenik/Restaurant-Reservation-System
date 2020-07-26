using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2RMSWS.Data;
using System.Dynamic;
using Newtonsoft.Json;

namespace T2RMSWS.Controllers
{
    // string to connect to React
    [Route("api/Scheduler")]
    [ApiController]
    public class ApiTableReservation : ControllerBase
    {
        private readonly ApplicationDbContext _db;


        public ApiTableReservation(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/ApiTableReservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableReservation>>> GetTableReserations()
        {
            return await _db.TableReservations.ToListAsync();
        }

        [HttpGet]
        [Route("Sittings")]
        public object GetSittings()
        {
            var calendarDate = DateTime.Today; //Convert.ToDateTime(date);
            //var calendarDate = Convert.ToDateTime(start);
            var sittings = _db.Sittings
                .Include(s => s.SittingType)
                .Where(s => s.StartDateTime > calendarDate && s.StartDateTime < calendarDate.AddDays(1))
                .ToArray();
            //create the sitting Type
            var sittingType = new ExpandoObject() as IDictionary<string, object>;

            foreach (var s in sittings)
            {
                //Add description of Sitting type
                var sitingDesc = new Dictionary<string, object>();
                sitingDesc["type"] = "resourceTimelineDay";
                sitingDesc["text"] = s.SittingType.Description;
                sitingDesc["minTime"] = s.StartDateTime.ToString("HH:mm:ss");
                sitingDesc["maxTime"] = s.EndDateTime.ToString("HH:mm:ss");
                //this creates a nested obj {"Breakfast"}:{sitingDesc..... }
                sittingType.Add(s.SittingType.Description, sitingDesc);
            }
       
            return sittingType;

        }

        [HttpGet]
        [Route("Reservations")]
        public async Task<object> GetReservations(string start)
        {
            var calendarDate = Convert.ToDateTime(start);
            var m = new Models.TableReservations.TableReservationsModel();
      
            m.TRs = await _db.Reservations
                .Include(r => r.TableReservations)
                .Where(r => r.StartDateTime >= calendarDate && r.StartDateTime < calendarDate.AddDays(1))
                .Where(r => r.ReservationStatusId != 3) // 3 is cancelled 
                .Select(r => new Models.TableReservations.TR
                {
                    Id = r.Id,
                    //ResourceId = r.TableReservations.Any() ? r.TableReservations.First().TableSittingId : 0,
                    ResourceId = r.TableReservations.FirstOrDefault().TableSitting.TableId,
                    Start = r.StartDateTime,
                    End = r.EndDateTime,
                    Title = $"Res-{r.Id}",
                    Guests = r.Guests,
                    Notes = r.Notes_Requirements,
                })
                .ToListAsync();

            foreach (var r in m.TRs)
            {
                if (r.ResourceId == null)
                {
                    r.ResourceId = 0;
                }
            }
            return m.TRs;
        }

        [HttpGet]
        [Route("SittingsEvent")]
        public async Task<object> GetSittingsEvent(string start)
        {
            var m = new Models.TableReservations.TableReservationsModel();
            var calendarDate = Convert.ToDateTime(start); 
            
            string[,] colours = new string[4, 2] { { "Breakfast", "#6495ed" },
                                                   { "Lunch", "#ed6495" },
                                                   { "Dinner", "#64edbc" }, 
                                                   { "Event" , "#edbc64" } };

            m.SittingEvents = await _db.TableSittings
                .Include(s => s.Table)
                .Include(s => s.Sitting)
                .Where(ts => ts.Sitting.StartDateTime > calendarDate && ts.Sitting.StartDateTime < calendarDate.AddDays(1))
                .Select(s => new Models.TableReservations.SittingEvent
                {
                    Title = s.Sitting.SittingType.Description,
                    Start = s.Sitting.StartDateTime,
                    End = s.Sitting.EndDateTime,
                    ResourceId = s.TableId,
                    Rendering = "background",
                    Color = "#6495ed",
                    GroupId = s.Id
                })
                .ToListAsync();

            foreach (var s in m.SittingEvents)
            {
                for (int i = 0; i < colours.GetLength(0); i++)
                {
                    if (s.Title == colours[i, 0])
                    {
                        s.Color = colours[i, 1];
                    }
                }
            }    

            return m.SittingEvents;
        }

        [HttpGet]
        [Route("Tables")]
        public async Task<object> GetTables(string start, string sitType)
        {
            var calendarDate = Convert.ToDateTime(start);
            var tables = await _db.TableSittings
                .Include(ts => ts.Sitting)
                .Include(ts => ts.Table)
                .ThenInclude(ts => ts.Area)
                .Where(r => r.Sitting.StartDateTime >= calendarDate && r.Sitting.StartDateTime < calendarDate.AddDays(1))
                .Select(ts => new {
                    id = ts.TableId,
                    area = ts.Table.Area.Description,
                    title = ts.Table.TableNo,
                    capacity = ts.Table.TableCapacity
                })
                .ToListAsync();
            tables.Insert(0, new { id = 0, area = "Unallocated", title = "Unallocated", capacity = 0}); ////added this to the db as tableId 0
            return tables;

        }
       

        [HttpPost,Route("assign-table")]
        public  async Task<object> AssignTable([FromBody]AssignTableParam p)
        {
            // get data from fullCalendar
            int resID = p.ReservationId;
            int dropTableId = p.DropTableId;
            int tableSitId = p.TableSittingId;
            
           // search for reservation startTime, sittingId and tableSitting 
            DateTime resDateTime = _db.Reservations
                .Where(r => r.Id == resID)
                .Select(r => r.StartDateTime)
                .FirstOrDefault();
            int sittingID = _db.Sittings
                .Where(s => resDateTime >= s.StartDateTime && resDateTime <= s.EndDateTime )
                .Select(s => s.Id)
                .First();
            int tableSittingId = _db.TableSittings
                .Where(ts => ts.SittingId == sittingID)
                .Where(ts => ts.TableId == dropTableId)
                .Select(ts => ts.Id)
                .First();

            //Add reservationId and tableSittingId to the tableReservation table in Database
            var tr = new TableReservation { ReservationId = resID, TableSittingId = tableSittingId };
            _db.TableReservations.Add(tr);
            // change reservation status to Confirmed 
            var res = _db.Reservations.FirstOrDefault(r => r.Id == resID);
            if (res.ReservationStatusId == 1)
            {
                res.ReservationStatusId = 2;
            }
            await _db.SaveChangesAsync();

            return Ok(tr); 
        }

        public class AssignTableParam
        {
            public int TableSittingId { get; set; }
            public int ReservationId { get; set; }
            public int DropTableId { get; set; }
            public DateTime ResEndDateTime { get; set; }
        }
        
       
    }
}
