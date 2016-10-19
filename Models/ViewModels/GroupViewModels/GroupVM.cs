

using System.Collections.Generic;

namespace community.Models.ViewModels.GroupViewModels {
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
        public int groupId {get; set;}
        public string title { get; set; }
        public string content {get; set;} 
    }
}