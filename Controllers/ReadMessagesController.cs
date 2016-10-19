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
using community.Models.ViewModels.ReadMessageViewModels;
using community.ListUtils;

namespace community.Controllers
{
    [Authorize]
    public class ReadMessagesController : Controller
    {

        UserManager<ApplicationUser> _userManager;
        public ReadMessagesController(
            UserManager<ApplicationUser> userManager
        )
        {
            _userManager = userManager;
        }


        /**
        * Index page for the Read messages, getting awesome data to display
        */
        public async Task<IActionResult> Index(ReadMessageIndexVM readMessageIndexVM)
        {

            System.Console.WriteLine("-----------hi ReadMessagesController:Index : ");
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                ReadMessageIndexVM rmIndexVm = BusinessFacade.GetUsersMessages(user);
                ReadInboxVM inbox = BusinessFacade.GetInboxInfo(user);

                return View(inbox);
            }
            else
            {
                System.Console.WriteLine("invalid model ");
                return View();
            }

        }
      
        /**
        * Handles call to display all messages 
        */
        [HttpPostAttribute]
        public async Task<IActionResult> ReadAllMessages()
        {

            System.Console.WriteLine("-----------hi ReadAllMessages : ");


            if (ModelState.IsValid)
            {
                //var user = await GetCurrentUserAsync();
                //ReadMessageIndexVM rmIndexVm = BusinessFacade.GetUsersMessages(user);
                var user = await GetCurrentUserAsync();
                ReadMessageIndexVM rmIndexVm = BusinessFacade.GetUsersMessages(user);
                return View("ReadMessages", rmIndexVm);
            }
            else
            {
                System.Console.WriteLine("invalid model ");
                return View();
            }
        }
        /**
        * Handles call to display all messages from specific user
        */
        [HttpPostAttribute]
        public async Task<IActionResult> ReadFromSender(ReadInboxVM senderId)
        {
            System.Console.WriteLine("-----------hi ReadFromSender : senderId = " + senderId.picked);
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                ReadMessageIndexVM rmIndexVm = BusinessFacade.GetUsersMessagesWithSender(user, senderId.picked);
                return View("ReadMessages", rmIndexVm);
            }
            else
            {
                System.Console.WriteLine("invalid model ");
                return View();
            }
        }
        
        /**
        * handles call for deleting a message
        */
        [HttpPostAttribute]
        public async Task<IActionResult> DeleteMessage([FromBodyAttribute]DeleteMessageVM vm)
        {
            bool deleted = false;
            if (ModelState.IsValid)
            {
                System.Console.WriteLine("-----------DeleteMessage : " + vm.ToString());
                var user = await GetCurrentUserAsync();
                deleted = BusinessFacade.DeleteMessage(vm, user);

            }
            else
            {
                System.Console.WriteLine("-----------GetMessageBody model invalid: ");
            }
            return Json(new { wasDeleted = deleted });
        }

        /**
        * call to get the content of a message
        */
        [HttpPostAttribute]
        public async Task<IActionResult> GetMessageBody([FromBodyAttribute]GetMessageBodyVM vm)
        {
            GetMessageBodyVM msgbody = null;
            if (ModelState.IsValid)
            {
                System.Console.WriteLine("-----------GetMessageBody : " + vm.ToString());
                var user = await GetCurrentUserAsync();

                var currentUserId = BusinessFacade.GetUserId(user);
                System.Console.WriteLine("User is: " + user.ToString() + " : " + currentUserId);
                System.Console.WriteLine("User Id: " + currentUserId);

                msgbody = BusinessFacade.GetMessageBody(vm, currentUserId);

            }
            else
            {
                System.Console.WriteLine("-----------GetMessageBody model invalid: ");
            }
            return Json(msgbody ?? new GetMessageBodyVM { id = 4, content = "this is from the controller" });
        }
        /**
        * get current user from HttpContext
        */
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            //return _userManager.GetUsersForClaimAsync(HttpContext.User);
            return _userManager.GetUserAsync(HttpContext.User);

        }
    }
}