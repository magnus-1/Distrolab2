using Microsoft.AspNetCore.Mvc;
using community.Models.EntryViewModels;

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
    }
}