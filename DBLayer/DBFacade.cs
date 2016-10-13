using community.Models.DBModels;
using community.Models.BusinessModels;

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

        public static GroupBL GetGroup(int groupId) {
            var groupDB = new DBLogic().GetGroup(groupId);
            return DBModelConverter.ConvertToGroupBL(groupDB);
        }
        public static void PostMessageToGroup(MessageBL msg,int groupId) 
        {
            new DBLogic().PostMessageToGroup(DBModelConverter.ConvertMessageBL(msg),groupId);
        }
    }
}
