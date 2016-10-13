
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

namespace community.Controllers {
    public class GroupController : Controller
    {
        public IActionResult MessageGroup() 
        {
            return RedirectToAction("EntryStart");
        }

        public IActionResult GroupPage() 
        {
            var group = BusinessFacade.GroupsWithKey(1);
            return View(group);
        }




        public IActionResult CreateGroup()
        {
            List<MessageVM> messages = new List<MessageVM>();
            for (int i = 0; i < 20; i++)
            {
                var data = new MessageVM{Title = "Title" + i, Content = "this is my text" + i, SenderName = "dude" + i };
                messages.Add(data);
                
            }
            var group = new GroupVM{Title = "Group title1", Messages = messages};
            BusinessFacade.InsertGroup(group);
            var group1 = new GroupVM{Title = "Group title2", Messages = messages};
            BusinessFacade.InsertGroup(group1);
            return RedirectToAction("GroupPage");
        }
    }
}