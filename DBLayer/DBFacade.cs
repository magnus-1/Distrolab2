using community.Models.DBModels;
using community.Models.BusinessModels;
using System.Collections.Generic;

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

        public static void InsertGroup(GroupBL group) {
            new DBLogic().InsertGroup(DBModelConverter.ConvertGroupBL(group));
        }


        public static GroupBL GetGroup(int groupId) {
            var groupDB = new DBLogic().GetGroup(groupId);
            return DBModelConverter.ConvertToGroupBL(groupDB);
        }

        public static List<GroupBL> GetGroups() {
            List<GroupDB> groupDBs = new DBLogic().GetGroups();
            return DBModelConverter.ConvertListToGroupBL(groupDBs);
        }

        public static void PostMessageToGroup(MessageBL msg,int groupId) 
        {
            new DBLogic().PostMessageToGroup(DBModelConverter.ConvertMessageBL(msg),groupId);
        }
    }
}
