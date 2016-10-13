namespace community.DBLayer { 

    public class DBFacade {

        public static string GetEntries(){
            return new DBLogic().GetEntries();
        }
        public static void InsertEntry(EntryDB entry){
            new DBLogic().InsertEntry(entry);
        }
    }
}