using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace community.Models.DBModels{
    public class GroupDB{

        public int Id { get; set; }
        public string Title {get;set;}

        public virtual List<MessageDB> Messages {get;set;}
        public virtual List<GroupMemberDB> GroupMembers {get;set;}
        
        public override string ToString(){
            return "GroupDB: Id = "+Id+", Title = "+Title+", Messages = "+ Messages;
        }
    }

    // core do not support many to many so we need to add a middle link
    public class GroupMemberDB {
        [Key]
        public int MembershipId {get;set;}
        public int GroupId { get; set; }
        public GroupDB Group { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}