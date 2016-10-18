
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace community.Models.BusinessModels {
    public class MessageBL {
        public int Id {get;set;}
        public string Title { get; set; }
        public string Content {get; set;}
        public bool IsRead {get; set;}
        public bool IsDeleted { get; set; }
        public ApplicationUser Sender {get;set;}
        public ApplicationUser Receiver {get;set} 
    }
}