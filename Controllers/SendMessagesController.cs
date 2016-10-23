
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
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            var user = await GetCurrentUserAsync();
            var currentUserId = user.Id;
            List<DestinationVM> destinfo = BusinessFacade.GetDestinations(user);//new List<DestinationVM>();
            foreach(DestinationVM d in destinfo) {
                System.Console.WriteLine("Index:GetDestinations: " + d.ToString());

            }
            
            SendMessageVM msg = new SendMessageVM{Tmptext ="tmptext",DestinationInfo = destinfo };
            System.Console.WriteLine("-----------hi Index : " +msg);
            return View(msg);
        }
        
        [HttpPostAttribute]
        public async Task<IActionResult> CreateMessage([FromBodyAttribute]NewMessageVM vm) {
            CreateMessageResponseVM response = null;
            bool success = false;
            string errorMessage = "";
            if(ModelState.IsValid) {
                System.Console.WriteLine("-----------CreateMessage : " + vm.ToString());
                System.Console.WriteLine("Destination size : " + vm.destinations.Count());
                foreach(DestinationVM dest in vm.destinations){
                    System.Console.WriteLine("destination : " + dest.ToString());
                }
                if(vm.destinations.Count > 0) {
                    var sender = await GetCurrentUserAsync();
                    response = BusinessFacade.SendNewMessage(vm,sender);
                    success = true;
                }else {
                    errorMessage = "no destination";
                }            

            }else {
                errorMessage = GetErrorMsg(ModelState.AsEnumerable());
                
                if(vm == null ||vm.destinations == null ||  vm.destinations.Count <= 0 ) {
                    errorMessage = errorMessage + " no destination";
                }
                System.Console.WriteLine( "CreateMessage error: " + errorMessage );
                System.Console.WriteLine("-----------CreateMessage model invalid: vm:  " + vm ?? "null");
            }
            return Json(new
            {
                wasSent = success,
                errorMsg = errorMessage,
                towho = response ?? new CreateMessageResponseVM { destinations = vm.destinations, title = "defaultTitle", timeStamp = "now i think..." }
            });
        }

        private string GetErrorMsg(IEnumerable<KeyValuePair<string, ModelStateEntry>> enumerable)
        {
            string errorMsg = "";
            foreach(var entry in enumerable) {
                errorMsg = errorMsg + entry.Key + " = ( ";
                foreach (var err in entry.Value.Errors.AsEnumerable())
                {
                    errorMsg = errorMsg + err.ErrorMessage + ", ";
                }
                errorMsg = errorMsg + " ),\n ";
            }
            return errorMsg;
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
            System.Console.WriteLine( "----------- Sendmessage controller :PostMessageToGroup" );
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