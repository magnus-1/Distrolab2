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
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                if (user != null)
                {
                    HomeVM info = BusinessFacade.GetHomeInfo(user);
                    return View(info);
                }
            }
            else
            {
                System.Console.WriteLine("Invalid model, redirect to login");
            }
            return RedirectToAction("Login", "AccountController");
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
