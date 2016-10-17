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
            System.Console.WriteLine("-----------hi Index : " );
            if (ModelState.IsValid)
            {
                List<ReadMessageVM> genmessages = new List<ReadMessageVM>();
                for (int i = 0; i < 10; i++)
                {
                    genmessages.Add(new ReadMessageVM
                    {
                        id = i,
                        isRead = false,
                        title = "title" + i,
                        time = "now o clock: " + i,
                        from = "from everone " + i
                    });

                }
                ReadMessageIndexVM vm = new ReadMessageIndexVM{messages = genmessages};
                return View(vm);
            }
            else
            {
                System.Console.WriteLine( "invalid model " );
                return View();
            }
            // var groupInfoVMs = BusinessFacade.GetGroups();

            // var groupVm = new GroupIndexVM { Groups = groupInfoVMs };
            // if(message != null) {
            //     System.Console.WriteLine("-----------hi Index : " + message.Tmptext);
            //     return View(message);
            // }
            // var user = await GetCurrentUserAsync();
            // var currentUserId = user.Id;
            // List<DestinationVM> destinfo = BusinessFacade.GetDestinations(user);//new List<DestinationVM>();
            // foreach (DestinationVM d in destinfo)
            // {
            //     System.Console.WriteLine("Index:GetDestinations: " + d.ToString());

            // }

            // for(int i = 0; i < 4;i++) {
            //     destinfo.Add(new DestinationVM{isGroup = false,
            //      destinationId = i,
            //     destinationName = "namn" + i});
            // }
           //SendMessageVM msg = new SendMessageVM { Tmptext = "tmptext", DestinationInfo = destinfo };
            
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);

        }
    }
}