

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace community.Models.ViewModels.GroupViewModels {
    /**
    *   Used to pass information to the cshtml view, 
    */
    public class GroupVM
    {
        public int GroupId {get; set;}

        public string Title {get; set;}
        public List<MessageVM> Messages { get; set; }

         public override string ToString(){
            return "GroupDB: Id = "+GroupId+", Title = "+Title+", Messages = "+ Messages;
        }
    }

    public class GroupPostMessage {
        [Required(ErrorMessage = "group id is wrong")]
        public int groupId { get; set; }
        [Required(ErrorMessage = "Need title")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "should be 2 to 60 in lenght")]
        public string title { get; set; }
        [Required(ErrorMessage = "Need to enter message")]
        public string content { get; set; }
        public override string ToString()
        {
            return "GroupPostMessage: Group: Id = " + groupId + ", Title = " + title + ", Messages = " + content;
        }
    }
}