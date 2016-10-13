
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

        public string SenderId {get;set;}
        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender {get;set;}

    }
}