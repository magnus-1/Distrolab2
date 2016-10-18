
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace community.Models.DBModels 
{
    public class LoginDB
    {
        [Key]
        public int Id {get;set;}

        [DataType(DataType.Time)]
        public DateTime TimeStamp {get; set;}

        public string UserId {get;set;}
        [ForeignKey("UserId")]
        public virtual ApplicationUser User {get;set;}

        public override string ToString (){
            return "Id= "+Id+"\n"+
            "TimeStamp= "+TimeStamp+"\n"+
            "UserId= "+UserId+"\n"+
            "User= "+User+"\n";
         
        }
    }
}