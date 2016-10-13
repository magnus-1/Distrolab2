using community.Models.DBModels;

namespace community.DBLayer { 

    public class DBFacade {

        public static string GetEntries(){
            return new DBLogic().GetEntries();
        }
        public static string EntriesWithKey(int key)
        {
            return new DBLogic().EntriesWithKey(key);
        }
        public static void InsertEntry(EntryDB entry){
            new DBLogic().InsertEntry(entry);
        }
    }
}
