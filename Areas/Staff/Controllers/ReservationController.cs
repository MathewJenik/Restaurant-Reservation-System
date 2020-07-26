using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace T2RMSWS.Areas.Staff.Controllers
{
    public class ReservationController : StaffAreaController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}