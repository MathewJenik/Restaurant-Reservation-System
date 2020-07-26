using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2RMSWS.Data;

namespace T2RMSWS.Areas.Member.Controllers
{
  
    public class HomeController : MemberAreaController
    {
        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
            :base(context, userManager)
        {

        }
        public IActionResult Index()
        {
            
            return View();
        }

        
    }
}