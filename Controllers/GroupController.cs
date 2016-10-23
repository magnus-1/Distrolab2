
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
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var groupInfoVMs = BusinessFacade.GetGroups(user);

            var groupVm = new GroupIndexVM { Groups = groupInfoVMs };

            return View(groupVm);
        }

        // public IActionResult GroupPage()
        // {
        //     var group = BusinessFacade.GroupsWithKey(1);
        //     return View(group);
        // }
        public async Task<IActionResult>  JoinGroup(int groupId)
        {
            System.Console.WriteLine("--------- JoinGroup with Id = " + groupId);
            //var group = BusinessFacade.GroupsWithKey(1);
            var user = await GetCurrentUserAsync();
            bool joined = BusinessFacade.JoinGroup(user , groupId);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> ViewGroup(int groupId)
        {
            System.Console.WriteLine("--------- ViewGroup with Id = " + groupId);
            var user = await GetCurrentUserAsync();
            if (BusinessFacade.IsGroupMember(user, groupId))
            {
                var group = BusinessFacade.GetGroupById(groupId);

                System.Console.WriteLine("--------- Gruppen vi ska till:  " + group);

                return View(group);
            }
            System.Console.WriteLine( "--------- ViewGroup with Id denied access" );
            return RedirectToAction("Index");
        }

        //public IActionResult CreateGroup(string groupTitle)
        [HttpPostAttribute]
        public IActionResult CreateGroup([FromBodyAttribute]NewGroupVM vm)
        {
            string errorMsg = "";
            bool wasCreated = false;
            GroupVM result = null;
            if (ModelState.IsValid)
            {
                string groupTitle = vm.title;
                System.Console.WriteLine("--------- CreateGroup with title = " + groupTitle);
                List<MessageVM> messages = new List<MessageVM>();

                var group = new GroupVM { Title = groupTitle, Messages = messages };
                result = BusinessFacade.InsertGroup(group);
                System.Console.WriteLine("Group to be returned to view: " + result);
                //return Json(result);
                wasCreated = true;
            }
            else
            {
                System.Console.WriteLine("Group; CreateGroup: invalid state");
                
            }
            return Json(new {wascreated = wasCreated,result = result,errormsg = errorMsg});
        }

//        public async Task<IActionResult> PostMessageToGroup(string title, string text, int groupId)
        [HttpPostAttribute]
        public async Task<IActionResult> PostMessageToGroup([FromBodyAttribute]GroupPostMessage vm)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var currentUserId = user.Id;
                System.Console.WriteLine("----------- Current User Id  = " + currentUserId);
                //System.Console.WriteLine( "PostMessageToGroup: Title: " + title + " text: " + text + " groupId: " + groupId );
                System.Console.WriteLine( "PostMessageToGroup:" + vm.ToString() );

                int Id = vm.groupId;
                //System.Console.WriteLine("--------- Input from ajax, message = " + text + " groupId = " + groupId);
                var messageSent = BusinessFacade.PostMessageToGroup(new MessageVM {Title = vm.title, Content = vm.content}, vm.groupId ,user);
                return Json(new {wasSent = true, message = messageSent,url = ""});
            }else {
                System.Console.WriteLine( "PostMessageToGroup: invalid state" );
            }
                return RedirectToAction("Index");
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);

        }
    }
}