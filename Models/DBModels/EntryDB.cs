
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace community.Models.DBModels 
{
    public class EntryDB 
    {
        [Key]
        public int Id {get;set;}
        public string NewsItem {get; set;}

    }
}