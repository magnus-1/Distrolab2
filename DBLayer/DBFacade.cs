using community.Models.DBModels;
using community.Models.BusinessModels;
using System.Collections.Generic;
using community.Models;
using System;

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

        public static GroupBL InsertGroup(GroupBL group) {
            var groupDB = new DBLogic().InsertGroup(DBModelConverter.ConvertGroupBL(group));
            return DBModelConverter.ConvertToGroupBL(groupDB);
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

        public static List<DestinationBL> GetUserGroupDestinations(ApplicationUser sender) {
            return new DBLogic().GetUserGroupDestinations(sender);
        }

        public static List<DestinationBL> GetUserDestinations(ApplicationUser sender) {
            return new DBLogic().GetUserDestinations(sender);
        }

        public static List<MessageBL> GetUsersMessages(ApplicationUser user)
        {
            List<MessageDB> msg =  new DBLogic().GetUsersMessages(user);
            return DBModelConverter.ListConvertToMessageBL(msg);
        }

        internal static List<MessageBL> GetUsersMessagesWithSender(ApplicationUser user, int senderId)
        {
            List<MessageDB> msg =  new DBLogic().GetUsersMessagesWithSender(user,senderId);
            return DBModelConverter.ListConvertToMessageBL(msg);
        }
        public static MessageBL ReadMessage(int sender,int messageId) {
            MessageDB msg =  new DBLogic().ReadMessage(sender,messageId);
            return DBModelConverter.ConvertToMessageBL(msg);
        }


        public static int GetUserId(ApplicationUser sender) {
            return new DBLogic().GetUserId(sender);
        }

        public static MessageBL SendMessage(int destinationId, MessageBL tmpMsg, ApplicationUser sender)
        {
            MessageDB messageDB = new DBLogic().SendMessage(destinationId,DBModelConverter.ConvertMessageBL(tmpMsg),sender); 
            return DBModelConverter.ConvertToMessageBL(messageDB);

        }
        public static List<DateTime> GetLoginTimeStamps(ApplicationUser user){
            return new DBLogic().GetLoginTimeStamps(user);
        }


         public static List<InboxBL>  GetConversations(ApplicationUser user) {
            List<InboxDB> inboxes = new DBLogic().GetConversations(user);
            return DBModelConverter.ConvertListToInboxBL(inboxes);
        }

        public static int GetNumberOfLogins(ApplicationUser user)
        {
            
            return new DBLogic().GetNumberOfLoginsThisMonth(user);
        }
    }
}
