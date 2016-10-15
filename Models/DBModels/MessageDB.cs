
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace community.Models.DBModels 
{
    public class MessageDB 
    {
        [Key]
        public int Id {get;set;}
        public string Content {get; set;}
        public bool IsRead {get; set;}
        public bool IsDeleted { get; set; }

        public string SenderId {get;set;}
        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender {get;set;}


        public override string ToString (){
            return "Id= "+Id+"\n"+
            "Content= "+Content+"\n"+
            "SenderId= "+SenderId+"\n"+ 
            "Sender= "+Sender; 
        }
    }
}