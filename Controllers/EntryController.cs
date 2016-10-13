using Microsoft.AspNetCore.Mvc;
using community.Models.EntryViewModels;
using community.Models.DBModels;
using community.Business;
using community.Controllers;
using Microsoft.AspNetCore.Routing;

namespace community.Controllers
{

    public class EntryController : Controller
    {
        //private BusinessFacade ctx = new BusinessFacade();
        
        public IActionResult EntryStart(string output)
        {
            NewsViewModel news = new NewsViewModel();
            System.Console.WriteLine("output = " + output);

            if (output == null)
            {
                news.NewsItem = BusinessFacade.GetEntries();
                return View(news);
            }
            news.NewsItem = output;
            return View(news);
        }

        public IActionResult EntryByKey()
        {
            NewsViewModel news = new NewsViewModel();
            news.NewsItem = BusinessFacade.GetEntryByKey(8);
            return View(news);

        }

        public IActionResult CreateEntries()
        {
            for (int i = 0; i < 20; i++)
            {
                var data = new EntryDB { NewsItem = "hej" + i };
                Business.BusinessFacade.InsertEntry(data);
            }
            return RedirectToAction("EntryStart");
        }
    }
}