using community.Models;
using Microsoft.AspNetCore.Identity;
using community.Data;
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
            EntryDB a =  ctx.Entries.First();
            System.Console.WriteLine("-------  db sak" + a.ToString());
            return a.NewsItem;
        }
        public string EntriesWithKey(int key)
        {
            var entry = ctx.Entries.Where(c => c.Id == key).ToList();
            return entry.First().NewsItem;//First().NewsItem;
        }

        public void InsertEntry(EntryDB entry) {
            ctx.Entries.Add(entry);
            ctx.SaveChanges();
            
        }
    }
}
