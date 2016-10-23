
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace community.Models.ViewModels.GroupViewModels
{
    /**
    *   Used to pass information to the cshtml view, 
    */
    public class GroupInfoVM
    {
        public int GroupId { get; set; }
        public string Title { get; set; }
        public bool JoinedGroup {get;set;}
    }

    public class NewGroupVM
    {
        [Required(ErrorMessage = "Need to enter destinations")]
        public string title { get; set; }
    }

    public class GroupIndexVM
    {
        public List<GroupInfoVM> Groups { get; set; }
    }

    


}