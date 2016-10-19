
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace community.Models.BusinessModels {
    public class InboxBL {
        public int UserId {get;set;}
        public int UnreadMessages {get;set;}
        public int DeletedMessages { get; set; } = 0;
        public int TotalMessages { get; set; } = 0;
        public ApplicationUser Sender {get;set;}

        public override string ToString (){
            return 
            "UserId= "+UserId+"\n"+
            "UnreadMessages= "+UnreadMessages+"\n"+
            "Sender= "+Sender + "\n"; 
        }

    }
    
}