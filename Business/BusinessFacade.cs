using community.Models.DBModels;
using community.Models.ViewModels;
using community.Models.ViewModels.GroupViewModels;

namespace community.Business
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class BusinessFacade
    {   
        public static string GetEntries() {
            var a = new BusinessLogic().GetEntries();
            return a;
        }

        public static string GetEntryByKey(int key){
            return new BusinessLogic().EntriesWithKey(key);
        }

        public static void InsertEntry(EntryDB entry) {
            new BusinessLogic().InsertEntry(entry);
        }

        public static void InsertGroup(GroupVM group) {
            new BusinessLogic().InsertGroup(BusinessModelConverter.ConvertGroupVM(group));
        }
        public GroupVM GroupsWithKey(int groupId)
        {
            var groupBL = new BusinessLogic().GroupsWithKey(groupId);
            return BusinessModelConverter.ConvertToGroupVM(groupBL);
        }

        public static void PostMessageToGroup(MessageVM msg,GroupVM groupVM) 
        {
            new BusinessLogic().PostMessageToGroup(BusinessModelConverter.ConvertMessageVM(msg) ,groupVM.GroupId );
        }
    }
}
