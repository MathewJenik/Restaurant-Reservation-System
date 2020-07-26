using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using T2RMSWS.Data;
using T2RMSWS.Models;

namespace T2RMSWS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db; 

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db; 
        }

        public IActionResult Index(int? capacity)
        {
            //Example 

            //var restaurant = _db.Restaurants.First();
            //var date = DateTime.Now.Date;
            //var breakfastSittingType = _db.SittingTypes.Find(1);
            //capacity = 1;
            //var mainArea = new List<Area>();
            //mainArea.Add(_db.Areas.First());
           

            //int daysIntoTheFuture = 7;
            //for (int i = 0; i < daysIntoTheFuture; i++)
            //{
            //    var s = new Sitting
            //    {
            //        StartDateTime = new DateTime(date.Year, date.Month, date.Day, 7, 0, 0),
            //        EndDateTime = new DateTime(date.Year, date.Month, date.Day, 11, 30, 0),
            //        Areas = mainArea,
            //        SittingType = breakfastSittingType,
            //        Capacity = capacity.HasValue ? capacity.Value : restaurant.Capacity,
            //        SittingStatusID = 1
            //    };
            //    restaurant.Sittings.Add(s);
            //    date = date.AddDays(1);
            //}

            //var people = _db.People.ToList(); 
            return View();
        }

        public IActionResult Test()
        {
            var sitting = _db.Sittings.FirstOrDefault(s => s.Id == 1);
            var d = sitting.StartDateTime;

            if (sitting == null)
            {
                return NotFound();
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString(),
       
            };
            

            var reservationType = _db.ReservationTypes.Find(1);

            var reservation = new Reservation
            {
                Customer = customer,
                StartDateTime = new DateTime(d.Year, d.Month, d.Day, 10, 15, 0),
                EndDateTime = new DateTime(d.Year, d.Month, d.Day, 10, 15, 0),
                Guests = 8,
                ReservationType = reservationType
    
            };
            
           // sitting.Reservations.Add(reservation);
            _db.SaveChanges();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Reservation() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RedirectUser()
        {

            if (User.IsInRole("Manager"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else if (User.IsInRole("Staff"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else if (User.IsInRole("Member"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return LocalRedirect("/");
        }
       
    }
}
