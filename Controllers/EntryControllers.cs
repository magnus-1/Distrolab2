using Microsoft.AspNetCore.Mvc;
using community.Models.EntryViewModels;
using community.Models.DBModels;
namespace community.Controllers {

    public class EntryController : Controller
    {
        //private BusinessFacade ctx = new BusinessFacade();
        public IActionResult EntryStart()
        {
            NewsViewModel news = new NewsViewModel();
            news.NewsItem = Business.BusinessFacade.GetHej();
            return View(news);
        }

        public IActionResult CreateEntries() {
            for(int i = 0; i < 20; i++) {
                var data = new EntryDB{NewsItem = "hej" + i};
                Business.BusinessFacade.InsertEntry(data);
            }
            return RedirectToAction("EntryStart");
        }
    }
}