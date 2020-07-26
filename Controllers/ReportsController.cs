using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using T2RMSWS.Data;
using T2RMSWS.Models.Reports;

namespace T2RMSWS.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ReportsDataModel reportsDataModel = new ReportsDataModel();

            // was overall reservations, now its within the last 24 hours
            //reportsDataModel.TotalReservations = _context.Reservations.ToList().Count();
            reportsDataModel.TotalReservations = _context.Reservations.Where(r => r.StartDateTime.Date == DateTime.Now.Date).ToList().Count();
            reportsDataModel.Seated = _context.Reservations.Where(r => r.StartDateTime.Date == DateTime.Now.Date && r.ReservationStatusId == 4).ToList().Count();
            reportsDataModel.Pending = _context.Reservations.Where(r => r.StartDateTime.Date == DateTime.Now.Date && r.ReservationStatusId == 1).ToList().Count();
            reportsDataModel.Cancelled = _context.Reservations.Where(r => r.StartDateTime.Date == DateTime.Now.Date && r.ReservationStatusId == 3).ToList().Count();


            // get the reservation table data per month (walk in, email, online)
            //                                              2,      4,      1
            for (int i = 0; i < 12; i++) {

                int[] monthData = new int[3];
                monthData[0] = _context.Reservations.Where(r => r.StartDateTime.Month == (i + 1) && r.ReservationTypeId == 2).ToList().Count();
                monthData[1] = _context.Reservations.Where(r => r.StartDateTime.Month == (i + 1) && r.ReservationTypeId == 4).ToList().Count();
                monthData[2] = _context.Reservations.Where(r => r.StartDateTime.Month == (i + 1) && r.ReservationTypeId == 1).ToList().Count();
                reportsDataModel.MonthTableData.Add(monthData);
            }


            //

            // get the yearly reservations
            var yearlyReservations = _context.Reservations.Where(r => r.StartDateTime.Year == DateTime.Now.Year);

            // set the requried data to the model
            for (int i = 0; i < 12; i++) {
                reportsDataModel.CurrentYearsReservationsByMonth.Add(yearlyReservations.Where(r=> r.StartDateTime.Month == (i+1)).Count());
            }

            var weeklyReservations = _context.Reservations.Where(r => r.StartDateTime.Day <= DateTime.Now.Day && r.StartDateTime.Day >= DateTime.Now.Day - 7);
            var startingDay = (int)DateTime.Now.DayOfWeek;

            for (int i = 0; i < 7; i++) {
                
                var counter = (weeklyReservations.Where(r => r.StartDateTime.Day == DateTime.Now.Day - 7 + i).Count());
                //reportsDataModel.CurrentWeeksReservations.Add(weeklyReservations.Where(r => r.StartDateTime.Day == DateTime.Now.Day -7 + i).Count());
                reportsDataModel.CurrentWeeksReservations.Add(counter);
            }

            //reportsDataModel.startingDay = (int)(weeklyReservations.First(r => (int)(r.StartDateTime.DayOfWeek) == 0).StartDateTime.DayOfWeek);

            for (int i = 0; i < 7; i++) {
                //var d = reportsDataModel.startingDay + i;
                var d = (int)DateTime.Now.DayOfWeek + i;
                if (d >= 7) {
                    d -= 7;
                }
                   // var d = reportsDataModel.startingDay + i;

                if (d == 0) {
                    reportsDataModel.DaysOfWeek.Add(0); // being Sunday
                } else if (d ==1) {
                    reportsDataModel.DaysOfWeek.Add(1); // being Monday
                }
                else if (d == 2)
                {
                    reportsDataModel.DaysOfWeek.Add(2); // being Tuesday
                }
                else if (d == 3)
                {
                    reportsDataModel.DaysOfWeek.Add(3); // being Wednesday
                }
                else if (d == 4)
                {
                    reportsDataModel.DaysOfWeek.Add(4); // being Thursday
                }
                else if (d == 5)
                {
                    reportsDataModel.DaysOfWeek.Add(5); // being Friday
                }
                else if (d == 6)
                {
                    reportsDataModel.DaysOfWeek.Add(6); // being Saturday
                }

                
            }



            reportsDataModel.Online = _context.Reservations
               .Where(r => r.StartDateTime >= DateTime.Today && r.StartDateTime < DateTime.Today.AddDays(1))
               .Where(r => r.ReservationTypeId == 1)
               .ToList()
               .Count();
            reportsDataModel.InPerson = _context.Reservations
               .Where(r => r.StartDateTime >= DateTime.Today && r.StartDateTime < DateTime.Today.AddDays(1))
               .Where(r => r.ReservationTypeId == 2)
               .ToList()
               .Count();
            reportsDataModel.Phone = _context.Reservations
               .Where(r => r.StartDateTime >= DateTime.Today && r.StartDateTime < DateTime.Today.AddDays(1))
               .Where(r => r.ReservationTypeId == 3)
               .ToList()
               .Count();
            reportsDataModel.Email = _context.Reservations
               .Where(r => r.StartDateTime >= DateTime.Today && r.StartDateTime < DateTime.Today.AddDays(1))
               .Where(r => r.ReservationTypeId == 4)
               .ToList()
               .Count();

                /* < th > ID </ th >
                < th > Name </ th >
                < th > Phone </ th >
                < th > Email </ th >*/

            // redirect to the required page and pass the model
            return View("Index", reportsDataModel);

        }

    }



}