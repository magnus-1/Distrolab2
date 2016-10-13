using community.DBLayer;
using community.Models.DBModels;
using community.Models.BusinessModels;
namespace community.Business
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class BusinessLogic
    {
        //private ApplicationDbContext ctx = ApplicationDbContext.Create();
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
        public void InsertGroup(GroupBL group) {
           DBFacade.InsertGroup(group);            
        }


        public GroupBL GroupsWithKey(int groupId)
        {
            return DBFacade.GetGroup(groupId);
        }

        public void PostMessageToGroup(MessageBL msg,int groupId) 
        {
            DBFacade.PostMessageToGroup(msg,groupId);
        }
    }
}
