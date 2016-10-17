using community.DBLayer;
using community.Models.DBModels;
using community.Models.BusinessModels;
using System.Collections.Generic;
using community.Models;

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


        public List<DestinationBL> GetDestinations(ApplicationUser sender) {
            List<DestinationBL> destinations = new List<DestinationBL>();
            var groups = DBFacade.GetUserGroupDestinations(sender);
            if(groups != null) {
                destinations.AddRange(groups);
            }
            var users = DBFacade.GetUserDestinations(sender);
            if(users != null) {
                destinations.AddRange(users);
            }
            return destinations;
        }
        public GroupBL GroupsWithKey(int groupId)
        {
            return DBFacade.GetGroup(groupId);
        }
        public List<GroupBL> GetGroups(){
            return DBFacade.GetGroups();
        }

        public void PostMessageToGroup(MessageBL msg,int groupId, ApplicationUser sender) 
        {   
            msg.Sender = sender;
            DBFacade.PostMessageToGroup(msg,groupId);
        }
    }
}
