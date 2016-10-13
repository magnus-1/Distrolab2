using community.Models;
using Microsoft.AspNetCore.Identity;
using community.Data;
using community.DBLayer;
using Microsoft.EntityFrameworkCore;
using community.Models.DBModels;
using System.Linq;
namespace community.Business
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class BusinessLogic
    {
        private ApplicationDbContext ctx = ApplicationDbContext.Create();
        public string GetEntries() {
            return DBFacade.GetEntries();
        }

        public string EntriesWithKey(int key)
        {
            return DBFacade.EntriesWithKey(key);
        }
        public void InsertEntry(EntryDB entry) {
            DBFacade.InsertEntry(entry);
            
        }
    }
}
