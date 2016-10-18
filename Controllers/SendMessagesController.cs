
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

        public IActionResult DummyPage(DummyVM dummy) {
            System.Console.WriteLine("----------- Hello from start : DummyPage");
            DummyVM tmp = new DummyVM{Name = "namndd"};
            System.Console.WriteLine("----------- DummyPage:tmp dummy : DummyPage" + tmp.ToString());
            return View( dummy ?? tmp);
        }
        [HttpPostAttribute]
        public IActionResult DummyPageData([FromBodyAttribute]DummyVM dummy) {
            //DummyVM dummy = new DummyVM{Name = Name, MyData = MyData, SomeDate = SomeDate};
            System.Console.WriteLine("----------- Hello from model : DummyPage");
            System.Console.WriteLine("----------- DummyPage: " + dummy.ToString());
            //System.Console.WriteLine("----------- DummyVM: " + Name + " : " + MyData + " : "+ SomeDate );
            if(ModelState.IsValid) {
                System.Console.WriteLine("----------- Valid model : " + dummy.ToString());
                //return Json("Success");
            }else {
                System.Console.WriteLine("----------- Invalid model : ");
            }
            return Json(dummy);//RedirectToAction("DummyPage",dummy);
        }

        // [HttpPostAttribute]
        // public IActionResult DummyPage([FromBodyAttribute]DummyVM dummy) {
        //     System.Console.WriteLine("----------- Hello from model : DummyPage");
        //     if(ModelState.IsValid) {
        //         System.Console.WriteLine("----------- Valid model : " + dummy.ToString());
        //         //return Json("Success");
        //     }else {
        //         System.Console.WriteLine("----------- Invalid model : ");
        //     }
        //     return View("DummyPage",dummy);
        // }




        public IActionResult SendMessage(SendMessageVM message) {
            
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