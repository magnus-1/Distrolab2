
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using community.Models;
using community.Models.AccountViewModels;
using community.Services;
using community.Models.ViewModels;
using community.Models.ViewModels.GroupViewModels;
using community.Business;
using System.Security.Claims;

namespace community.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {

        UserManager<ApplicationUser> _userManager;
        public GroupController(
            UserManager<ApplicationUser> userManager
        )
        {
            _userManager = userManager;
        }

        public IActionResult MessageGroup()
        {
            return RedirectToAction("EntryStart");
        }
        public IActionResult Index()
        {
            var groupInfoVMs = BusinessFacade.GetGroups();

            var groupVm = new GroupIndexVM { Groups = groupInfoVMs };

            return View(groupVm);
        }

        // public IActionResult GroupPage()
        // {
        //     var group = BusinessFacade.GroupsWithKey(1);
        //     return View(group);
        // }
        public IActionResult JoinGroup(int groupId)
        {
            System.Console.WriteLine("--------- JoinGroup with Id = " + groupId);
            //var group = BusinessFacade.GroupsWithKey(1);
            return RedirectToAction("Index");
        }


        public IActionResult ViewGroup(int groupId)
        {
            System.Console.WriteLine("--------- ViewGroup with Id = " + groupId);
            var group = BusinessFacade.GetGroupById(groupId);
            return View(group);
        }

        public IActionResult CreateGroup(string groupTitle)
        {
            System.Console.WriteLine("--------- CreateGroup with title = " + groupTitle);
            List<MessageVM> messages = new List<MessageVM>();
            // for (int i = 0; i < 20; i++)
            // {
            //     var data = new MessageVM { Title = "Title" + i, Content = "this is my text" + i, SenderName = "dude" + i };
            //     messages.Add(data);

            // }
            var group = new GroupVM { Title = groupTitle, Messages = messages };
            BusinessFacade.InsertGroup(group);

            return RedirectToAction("Index");
        }

        [HttpPostAttribute]
        public async Task<IActionResult> PostMessageToGroup(string text, int groupId)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var currentUserId = user.Id;
                System.Console.WriteLine("----------- Current User Id  = " + currentUserId);

                int Id = groupId;
                System.Console.WriteLine("--------- Input from ajax, message = " + text + " groupId = " + groupId);
                BusinessFacade.PostMessageToGroup(new MessageVM { Content = text}, groupId,user);
                return RedirectToAction("ViewGroup", new { groupId = Id });
            }
                return RedirectToAction("Index");
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);

        }
    }
}