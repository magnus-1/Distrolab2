using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace community.Models.DBModels 
{
    public class InboxDB 
    {
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

     /**
        * Includes extra information about deleted messages and totalmessages per sender 
        */
        public class UserSenderDB
        {
            public int UserId { get; set; }
            public ApplicationUser Sender { get; set; }
            public int UnreadMessages { get; set; }
            public int DeletedMessages { get; set; }
            public int TotalMessages { get; set; }
        }
}