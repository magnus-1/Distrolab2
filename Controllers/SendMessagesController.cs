
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
    public class SendMessagesController : Controller
    {

        UserManager<ApplicationUser> _userManager;
        public SendMessagesController(
            UserManager<ApplicationUser> userManager
        )
        {
            _userManager = userManager;
        }

        public IActionResult MessageGroup()
        {
            return RedirectToAction("EntryStart");
        }
       
        public async Task<IActionResult> Index(SendMessageVM message)
        {
            // var groupInfoVMs = BusinessFacade.GetGroups();

            // var groupVm = new GroupIndexVM { Groups = groupInfoVMs };
            // if(message != null) {
            //     System.Console.WriteLine("-----------hi Index : " + message.Tmptext);
            //     return View(message);
            // }
            var user = await GetCurrentUserAsync();
            var currentUserId = user.Id;
            List<DestinationVM> destinfo = BusinessFacade.GetDestinations(user);//new List<DestinationVM>();
            foreach(DestinationVM d in destinfo) {
                System.Console.WriteLine("Index:GetDestinations: " + d.ToString());

            }
            
            // for(int i = 0; i < 4;i++) {
            //     destinfo.Add(new DestinationVM{isGroup = false,
            //      destinationId = i,
            //     destinationName = "namn" + i});
            // }
            SendMessageVM msg = new SendMessageVM{Tmptext ="tmptext",DestinationInfo = destinfo };
            System.Console.WriteLine("-----------hi Index : " +msg);
            return View(msg);
        }
        
        [HttpPostAttribute]
        public async Task<IActionResult> CreateMessage([FromBodyAttribute]NewMessageVM vm) {
            CreateMessageResponseVM response = null;
            if(ModelState.IsValid) {
                System.Console.WriteLine("-----------CreateMessage : " + vm.ToString());
                System.Console.WriteLine("Destination size : " + vm.destinations.Count());
                
                foreach(DestinationVM dest in vm.destinations){
                    System.Console.WriteLine("destination : " + dest.ToString());
                }
                var sender = await GetCurrentUserAsync();
                response = BusinessFacade.SendNewMessage(vm,sender);

            }else {
                System.Console.WriteLine("-----------CreateMessage model invalid: " );
            }
            return Json(response ?? new CreateMessageResponseVM{destinations = vm.destinations,title = "defaultTitle", timeStamp = "now i think..."});
        }


        public IActionResult SendMessage(SendMessageVM message) {
            
            return RedirectToAction("Index");
        }

        public IActionResult ViewGroup(int groupId)
        {
            System.Console.WriteLine("--------- ViewGroup with Id = " + groupId);
            var group = BusinessFacade.GetGroupById(groupId);
            return View(group);
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