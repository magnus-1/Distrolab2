

using System.Linq;
using community.Data;
using community.Models.DBModels;

namespace community.DBLayer {

    public class DBLogic {


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