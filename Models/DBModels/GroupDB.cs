using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace community.Models.DBModels{
    public class GroupDB{


        public int Id { get; set; }
        public string Title {get;set;}

        public virtual List<MessageDB> Messages {get;set;}
        

    }
}