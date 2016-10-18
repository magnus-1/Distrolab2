
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace community.Models.BusinessModels {
    public class InboxBL {
        public int UserId {get;set;}
        public int UnreadMessages {get;set;}
        public ApplicationUser Sender {get;set;}

        public override string ToString (){
            return 
            "UserId= "+UserId+"\n"+
            "UnreadMessages= "+UnreadMessages+"\n"+
            "Sender= "+Sender + "\n"; 
        }

    }
    
}