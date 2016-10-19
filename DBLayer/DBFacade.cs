using community.Models.DBModels;
using community.Models.BusinessModels;
using System.Collections.Generic;
using community.Models;
using System;

namespace community.DBLayer
{

    public class DBFacade
    {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                    REMOVE ???????
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetEntries()
        {
            return new DBLogic().GetEntries();
        }
        public static string EntriesWithKey(int key)
        {
            return new DBLogic().EntriesWithKey(key);
        }
        public static void InsertEntry(EntryDB entry)
        {
            new DBLogic().InsertEntry(entry);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////


        /**
        * Calls to insert group to dadabase. 
        * Returns DBobject converted to BLobject
        */
        public static GroupBL InsertGroup(GroupBL group)
        {
            var groupDB = new DBLogic().InsertGroup(DBModelConverter.ConvertGroupBL(group));
            return DBModelConverter.ConvertToGroupBL(groupDB);
        }

        /**
        * Calls to get group from database. 
        * Returns DBobject converted to BLobject
        */
        public static GroupBL GetGroup(int groupId)
        {
            var groupDB = new DBLogic().GetGroup(groupId);
            return DBModelConverter.ConvertToGroupBL(groupDB);
        }

        /**
        * Calls to get all groups from database. 
        * Returns DBobject List converted to BLobject List
        */
        public static List<GroupBL> GetGroups()
        {
            List<GroupDB> groupDBs = new DBLogic().GetGroups();
            return DBModelConverter.ConvertListToGroupBL(groupDBs);
        }

        /**
        * Calls to insert message to group in database. 
        * Returns DBobject converted to BLobject
        */
        public static MessageBL PostMessageToGroup(MessageBL msg, int groupId)
        {
            MessageDB messageDB = new DBLogic().PostMessageToGroup(DBModelConverter.ConvertMessageBL(msg), groupId);
            return DBModelConverter.ConvertToMessageBL(messageDB);
        }

        /**
        * Calls to get all group destinations for specific user from database. 
        * Returns DBobject List converted to BLobject List
        */
        public static List<DestinationBL> GetUserGroupDestinations(ApplicationUser sender)
        {
            return new DBLogic().GetUserGroupDestinations(sender);
        }

        /**
       * Calls to get all user destinations for specific user from database. 
       * Returns DBobject List converted to BLobject List
       */
        public static List<DestinationBL> GetUserDestinations(ApplicationUser sender)
        {
            return new DBLogic().GetUserDestinations(sender);
        }

        /**
       * Calls to get all messages for specific user from database. 
       * Returns DBobject List converted to BLobject List
       */
        public static List<MessageBL> GetUsersMessages(ApplicationUser user)
        {
            List<MessageDB> msg = new DBLogic().GetUsersMessages(user);
            return DBModelConverter.ListConvertToMessageBL(msg);
        }

        /**
       * Calls to get all messages from specific user to specific user from database. 
       * Returns DBobject List converted to BLobject List
       */
        internal static List<MessageBL> GetUsersMessagesWithSender(ApplicationUser user, int senderId)
        {
            List<MessageDB> msg = new DBLogic().GetUsersMessagesWithSender(user, senderId);
            return DBModelConverter.ListConvertToMessageBL(msg);
        }

        /**
       * Calls to add group to users groupList in database. 
       * Returns boolean of success or failure
       */
        internal static bool JoinGroup(ApplicationUser user, int groupId)
        {
            return new DBLogic().JoinGroup(user, groupId);
        }

        /**
        * Calls to get a message and mark it as read from database. 
        * Returns DBobject converted to BLobject
        */
        public static MessageBL ReadMessage(int sender, int messageId)
        {
            MessageDB msg = new DBLogic().ReadMessage(sender, messageId);
            return DBModelConverter.ConvertToMessageBL(msg);
        }

        /**
        * Calls to get the integer version of userId from database. 
        * Returns int Id
        */
        public static int GetUserId(ApplicationUser sender)
        {
            return new DBLogic().GetUserId(sender);
        }

        /**
        * Calls to insert a message to a users received messages. 
        * Returns message DBobject converted to message BLobject
        */
        public static MessageBL SendMessage(int destinationId, MessageBL tmpMsg, ApplicationUser sender)
        {
            MessageDB messageDB = new DBLogic().SendMessage(destinationId, DBModelConverter.ConvertMessageBL(tmpMsg), sender);
            return DBModelConverter.ConvertToMessageBL(messageDB);

        }
        /**
        * Calls to get all login timestamps for specific user from database. 
        * Returns DBobject list converted to BLobject list
        */
        public static List<DateTime> GetLoginTimeStamps(ApplicationUser user)
        {
            return new DBLogic().GetLoginTimeStamps(user);
        }
        /**
        * Calls to mark spwcific message as deleted in database. 
        * Returns boolean succes/failure
        */
        internal static bool DeleteMessage(int messageId, ApplicationUser user)
        {
            return new DBLogic().DeleteMessage(messageId, user);
        }
        /**
        * Calls to get all conversations for specific user from database. 
        * Returns DBobject list converted to BLobject list
        */
        public static List<InboxBL> GetConversations(ApplicationUser user)
        {
            List<InboxDB> inboxes = new DBLogic().GetConversations(user);
            return DBModelConverter.ConvertListToInboxBL(inboxes);
        }
        /**
        * Calls to get number of logins this month from database. 
        * Returns int Id
        */
        public static int GetNumberOfLogins(ApplicationUser user)
        {

            return new DBLogic().GetNumberOfLoginsThisMonth(user);
        }
    }
}
