using System.Threading.Tasks;
using community.Business;
using community.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using community.Models;


namespace community.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
    
     
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync(); 
            HomeVM info = BusinessFacade.GetHomeInfo(user);
            
            return View(info);
        }

        public IActionResult Error()
        {
            return View();
        }
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);

        }
    }
}
