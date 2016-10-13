
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace community.Models.BusinessModels {
    public class GroupBL {
        public int Id { get; set; }
        public string Title {get;set;}

        public List<MessageBL> Messages {get;set;}
        

    }
}