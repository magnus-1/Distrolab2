
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace community.Models.BusinessModels {
    public class MessageBL {
        public int Id {get;set;}

        public string Title {get; set;}

        public string Content {get; set;}
        public bool IsRead {get; set;}
        public bool IsDeleted { get; set; }
        public ApplicationUser Sender {get;set;}
        
        public override string ToString (){
            return "Id= "+Id+"\n"+
            "IsRead= "+IsRead+"\n"+
            "IsDeleted= "+IsDeleted+"\n"+
            "Title= "+Title+"\n"+
            "Content= "+Content+"\n"+
            "Sender= "+Sender; 
        }
        public ApplicationUser Receiver {get;set;} 
    }
}