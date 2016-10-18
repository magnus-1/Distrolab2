using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using community.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace community.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {

            return View(new HomeVM {UserName = "Calle"});
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
