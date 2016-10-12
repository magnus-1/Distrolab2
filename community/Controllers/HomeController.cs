using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Distrolab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Distrolab2.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDBContext ctx = new ApplicationDBContext();

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult createConference (){
            var conference = new Conference {
                Name="Second Conference",
                TicketPrice=250.00m

            };
            ctx.Conferences.Add(conference);
            ctx.SaveChanges();

            var sessionTitles = new List<string> {
                "nr1","nr2","nr3"
            };

            foreach (var title in sessionTitles){
                var session = new Session{
                    Title = title,
                    Conference = conference
                };

                ctx.Sessions.Add(session);
                ctx.SaveChanges();
            }
            return RedirectToAction("ViewConference");
        }
        public IActionResult ViewConference(){
            var conference = ctx.Conferences.Include(c => c.Sessions).First();
            System.Console.WriteLine("Conferece from database: " + conference.ToString());
            return View(conference);
        }
    }
}
