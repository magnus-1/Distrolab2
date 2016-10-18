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



        public async Task<IActionResult> Index(ReadMessageIndexVM readMessageIndexVM)
        {
            System.Console.WriteLine("-----------hi ReadMessagesController:Index : ");
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                ReadMessageIndexVM rmIndexVm = BusinessFacade.GetUsersMessages(user);
                ReadInboxVM inbox = new ReadInboxVM
                {
                    incomingFrom = ListConverter.Map(
                        rmIndexVm.messages, m => new FromUser
                        {
                            senderId = 22,
                            senderName = m.from,
                            recevedCount = 3
                        })
                };
                return View(inbox);
            }
            else
            {
                System.Console.WriteLine("invalid model ");
                return View();
            }

            return View();
        }

        [HttpPostAttribute]
        public async Task<IActionResult> ReadFromSender(int? senderId)
        {
            System.Console.WriteLine("-----------hi ReadFromSender : senderId = " + senderId);
            if (ModelState.IsValid)
            {
                //var user = await GetCurrentUserAsync();
                //ReadMessageIndexVM rmIndexVm = BusinessFacade.GetUsersMessages(user);
                return RedirectToAction("ReadMessages");
            }
            else
            {
                System.Console.WriteLine("invalid model ");
                return View();
            }
        }

        public async Task<IActionResult> ReadMessages(ReadMessageIndexVM readMessageIndexVM)
        {
            System.Console.WriteLine("-----------hi Index : ");
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                ReadMessageIndexVM rmIndexVm = BusinessFacade.GetUsersMessages(user);
                return View(rmIndexVm);
            }
            else
            {
                System.Console.WriteLine("invalid model ");
                return View();
            }

            return View();
        }


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

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            //return _userManager.GetUsersForClaimAsync(HttpContext.User);
            return _userManager.GetUserAsync(HttpContext.User);

        }
    }
}