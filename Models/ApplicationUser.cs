using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using community.Models.DBModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace community.Models
{

    public class ApplicationUser : IdentityUser
    {

        public virtual List<GroupMemberDB> GroupMembership { get; set; }
        public virtual List<MessageDB> ReceivedMessages { get; set; }
        public virtual List<MessageDB> SentMessages { get; set; }
    
        public UserIdDB UserId {get;set;}

    }
}
