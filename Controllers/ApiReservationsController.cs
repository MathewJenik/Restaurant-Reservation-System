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
    [Route("api/reservations")]
    [ApiController]
    public class ApiReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ApiReservationsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> ReservationByEmailAsync(string email)
        {
            var person = await _db.People.Where(p => p.Email == email).FirstOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }
            var result = await _db.Reservations
                .Include(r => r.TableReservations)
                .Include(r => r.ReservationStatus)
                .Include(r => r.ReservationType)
                .Where(r => r.Customer.Id == person.Id)        
                .ToListAsync();

            return Ok(result);
        }

        /*[HttpGet("{id}")]
        public async Task<ActionResult<object>> GetReservation(int id)
        {
            var reservation = await _db.Reservations
                .Include(r => r.Sitting)
                    .ThenInclude(s => s.Restaurant)
                .Include(r => r.Sitting)
                    .ThenInclude(s => s.SittingType)
                .Include(r => r.ReservationType)
                .Include(r => r.ReservationStatus)
                .Include(r => r.Customer)
                .Where(r => r.Id == id)
                .Select(r => new
                {
                    Reference = r.Id,
                    Restaurant = r.Sitting.Restaurant.Name,
                    Customer_Id = $"ID: {r.Customer.Id}",
                    Customer_Name = ($"{r.Customer.FirstName} {r.Customer.LastName}"),
                    SittingStart = r.Sitting.StartDateTime,
                    SittingEnd = r.Sitting.EndDateTime,
                    r.Guests,
                    r.DateTime,
                    r.ReservationStatus
                })
                .FirstOrDefaultAsync();

            return reservation;
        }*/
    }
}