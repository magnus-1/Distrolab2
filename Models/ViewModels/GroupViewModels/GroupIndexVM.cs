
using System.Collections.Generic;

namespace community.Models.ViewModels.GroupViewModels
{
    public class GroupInfoVM
    {
        public int GroupId { get; set; }
        public string Title { get; set; }
        public bool JoinedGroup {get;set;}
    }

    public class GroupIndexVM
    {
        public List<GroupInfoVM> Groups { get; set; }
    }

    


}