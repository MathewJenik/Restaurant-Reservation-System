using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace T2RMSWS.Areas.Management.Controllers
{
    public class ReservationController : ManagerAreaController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}