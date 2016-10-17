
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace community.Models.BusinessModels {
    public class DestinationBL {
        public int Id {get;set;}
        public string Name {get;set;}
        public bool IsGroup {get;set;}
    }
}