namespace community.DBLayer {


    public class DBLogic {


        private ApplicationDbContext ctx = ApplicationDbContext.Create();
        public string GetEntries() {
            EntryDB a =  ctx.Entries.First();
            System.Console.WriteLine("-------  db sak" + a.ToString());
            return a.NewsItem;
        }
        
        public void InsertEntry(EntryDB entry) {
            ctx.Entries.Add(entry);
            ctx.SaveChanges();
        }
    }
}