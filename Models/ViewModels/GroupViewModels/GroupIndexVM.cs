
using System.Collections.Generic;

namespace community.Models.ViewModels.GroupViewModels {
    public class GroupVM
    {
       List<GroupInfoVM> Groups {get;set;}
    
    }

    public class GroupInfoVM
    {
        public int GroupId {get; set;}
        public string Title {get; set;}
    }

}