using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using community.Models.DBModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace community.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        
        public virtual List<GroupDB> Groups {get;set;}
        public virtual List<MessageDB> ReceivedMessages {get;set;}
        public virtual List<MessageDB> SentMessages {get;set;}
        
    }
}
