using System;
using System.Collections.Generic;
using community.DBLayer;
using community.Models;
using community.Models.BusinessModels;
using community.Models.DBModels;
using community.Models.ViewModels;
using community.Models.ViewModels.GroupViewModels;
using community.Models.ViewModels.ReadMessageViewModels;

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

        internal static HomeVM GetHomeInfo(ApplicationUser user)
        {
           return new BusinessLogic().GetHomeInfo(user);
        }

        public static void InsertEntry(EntryDB entry) {
            new BusinessLogic().InsertEntry(entry);
        }

        public static GroupVM InsertGroup(GroupVM group) {
            var groupBL = new BusinessLogic().InsertGroup(BusinessModelConverter.ConvertGroupVM(group));
            return BusinessModelConverter.ConvertToGroupVM(groupBL);
        }
        public static List<GroupInfoVM> GetGroups() {
        
            return BusinessModelConverter.ConvertListToGroupInfoVM(new BusinessLogic().GetGroups());
        
        }

        public static ReadMessageIndexVM GetUsersMessages(ApplicationUser user)
        {
            List<MessageBL> msg = new BusinessLogic().GetUsersMessages(user);
            return BusinessModelConverter.ConvertToReadMessageIndexVM(msg);
        }
        public static int GetUsersUnreadMessagesCount(ApplicationUser user) {
            return new BusinessLogic().GetUsersUnreadMessagesCount(user);
        }

        internal static bool JoinGroup(ApplicationUser user, int groupId)
        {
            return new BusinessLogic().JoinGroup(user,groupId);
        }

        internal static ReadMessageIndexVM GetUsersMessagesWithSender(ApplicationUser user, int senderId)
        {
            List<MessageBL> msg = new BusinessLogic().GetUsersMessagesWithSender(user,senderId);
            return BusinessModelConverter.ConvertToReadMessageIndexVM(msg);
        }


        public static List<DestinationVM> GetDestinations(ApplicationUser sender) {
            var destinations = new BusinessLogic().GetDestinations(sender);
            return BusinessModelConverter.ConvertToDestinationVM(destinations);
        }


        public static GroupVM GetGroupById(int groupId)
        {
            var groupBL = new BusinessLogic().GroupsWithKey(groupId);
            return BusinessModelConverter.ConvertToGroupVM(groupBL);
        }


        public static GetMessageBodyVM GetMessageBody(GetMessageBodyVM vm,int reader) {
            var msgBody = BusinessModelConverter.ConvertGetMessageBodyVM(vm);
            MessageBodyBL msg = new BusinessLogic().ReadMessageBody(msgBody,reader);
            return BusinessModelConverter.ConvertToGetMessageBodyVM(msg);
        }

    
        public static void PostMessageToGroup(MessageVM msg,int groupId, ApplicationUser sender ) 
        {
            new BusinessLogic().PostMessageToGroup(BusinessModelConverter.ConvertMessageVM(msg) ,groupId, sender);
        }

        public static int GetUserId(ApplicationUser sender) {
            return DBFacade.GetUserId(sender);
        }

        public static CreateMessageResponseVM SendNewMessage(NewMessageVM vm, ApplicationUser sender)
        {
            CreateMessageResponseVM response = new BusinessLogic().SendNewMessage(vm,sender);
            return response;
        }
        public static ReadInboxVM GetConversations(ApplicationUser user) 
        {
           List<InboxBL> inboxes = new BusinessLogic().GetConversations(user);
           ReadInboxVM inbox = BusinessModelConverter.ConvertInboxListToInboxVM(inboxes);
           return inbox;

        }
    }
}
