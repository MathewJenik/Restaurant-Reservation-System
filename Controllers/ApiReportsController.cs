using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2RMSWS.Data;

namespace T2RMSWS.Controllers
{
    
    [Route("api/Dashboard")]
    [ApiController]
    public class ApiReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ApiReportsController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: api/ApiTableReservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetTableReserations()
        {
            return await _db.Reservations.ToListAsync();
        }

        [HttpGet, Route("BookingType")]
        public int BookingType(int TypeId)
        {

            var bookings = _db.Reservations
               .Include(r => r.ReservationType)
               .Where(r => r.StartDateTime >= DateTime.Today && r.StartDateTime < DateTime.Today.AddDays(1))
               .Where(r => r.ReservationTypeId == TypeId)
               .Select(r => new Models.Reports.BookingType
               {
                   Description = r.ReservationType.Description,
               }).ToList().Count();

               return bookings;
        }
        [HttpGet, Route("WeeklyCapacity")]
        public async Task<object> WeeklyCapacity()
        {
            DateTime mondayOfWeek = DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek);
            DateTime sundayOfWeek = DateTime.Now.AddDays((DayOfWeek.Sunday + 7) - DateTime.Now.DayOfWeek);
            var Guests = _db.Reservations
                .Where(r => r.StartDateTime >= mondayOfWeek && r.StartDateTime <= sundayOfWeek)
                .AsEnumerable()
                .GroupBy(r => r.StartDateTime.DayOfWeek)
                .OrderBy(r => r.Key)
                .Select(r => new 
                {
                    DayOfWeek = r.Key,
                    Guests = r.Sum(r => r.Guests)
                }).ToArray();
            var Capacity = _db.Sittings
                .Where(s => s.StartDateTime >= mondayOfWeek && s.StartDateTime <= sundayOfWeek)
                //.AsEnumerable()
                //.GroupBy(s => new { s.StartDateTime.DayOfWeek, s.Capacity, s.SittingTypeId })
                .Select(s => new 
                {
                    s.StartDateTime.DayOfWeek,
                    s.Capacity,
                    SitingTypeId = s.SittingTypeId
                }).ToArray();

            /*var totalCap = weeklyCap.GroupBy(c => c.DayOfWeek).Select(c => new
            {
             cap = c.Sum(c => c.Guests) / c.Sum(c => c.Capacity) *100
            }).ToArray();*/

            //return Guests;
            return Capacity;
               
        }
    }
}