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


        /**
        * forwarding request to get all info for the home page, returns HomeInfoVM
        */
        internal static HomeVM GetHomeInfo(ApplicationUser user)
        {
            return new BusinessLogic().GetHomeInfo(user);
        }

        /**
        * forwarding request to insert group into db, returns groupVM
        */
        public static GroupVM InsertGroup(GroupVM group)
        {
            var groupBL = new BusinessLogic().InsertGroup(BusinessModelConverter.ConvertGroupVM(group));
            return BusinessModelConverter.ConvertToGroupVM(groupBL);
        }

        /**
        * forwarding request to get all groups, returns list of GroupVMs 
        */
        public static List<GroupInfoVM> GetGroups(ApplicationUser user) {
        
            return BusinessModelConverter.ConvertListToGroupInfoVM(new BusinessLogic().GetGroups(user));
        
        }


        /**
        * forwarding request to get all messages from a user, returns messageVMs
        */
        public static ReadMessageIndexVM GetUsersMessages(ApplicationUser user)
        {
            List<MessageBL> msg = new BusinessLogic().GetUsersMessages(user);
            return BusinessModelConverter.ConvertToReadMessageIndexVM(msg);
        }
        /**
        * forwarding request to get the number of unread messages
        */
        public static int GetUsersUnreadMessagesCount(ApplicationUser user)
        {
            return new BusinessLogic().GetUsersUnreadMessagesCount(user);
        }

        internal static bool IsGroupMember(ApplicationUser user, int groupId)
        {
            return new BusinessLogic().IsGroupMember(user, groupId);
        }

        /**
        * forwarding request to join a group
        */
        internal static bool JoinGroup(ApplicationUser user, int groupId)
        {
            return new BusinessLogic().JoinGroup(user, groupId);
        }

        /**
        * forwarding request to get Inbox FROM specific user to current user
        */
        internal static ReadMessageIndexVM GetUsersMessagesWithSender(ApplicationUser user, int senderId)
        {
            List<MessageBL> msg = new BusinessLogic().GetUsersMessagesWithSender(user, senderId);
            return BusinessModelConverter.ConvertToReadMessageIndexVM(msg);
        }

        /**
        * forwarding request to get available destinations, returning converted list to DestinationVMs
        */
        public static List<DestinationVM> GetDestinations(ApplicationUser sender)
        {
            var destinations = new BusinessLogic().GetDestinations(sender);
            return BusinessModelConverter.ConvertToDestinationVM(destinations);
        }

        /**
        * forwarding request to get group matching id
        */
        public static GroupVM GetGroupById(int groupId)
        {
            var groupBL = new BusinessLogic().GroupsWithKey(groupId);
            return BusinessModelConverter.ConvertToGroupVM(groupBL);
        }

        /**
        * forwarding request to get the message content, some convertings happens here, no real magic
        */
        public static GetMessageBodyVM GetMessageBody(GetMessageBodyVM vm, int reader)
        {
            var msgBody = BusinessModelConverter.ConvertGetMessageBodyVM(vm);
            MessageBodyBL msg = new BusinessLogic().ReadMessageBody(msgBody, reader);
            return BusinessModelConverter.ConvertToGetMessageBodyVM(msg);
        }

        /**
        * forwarding request to post message to group, coverting VM message to BL befor sending to BLLogic
        */
        public static void PostMessageToGroup(MessageVM msg, int groupId, ApplicationUser sender)
        {
            new BusinessLogic().PostMessageToGroup(BusinessModelConverter.ConvertMessageVM(msg), groupId, sender);
        }

        /**
        * forwarding request to get Integer userId
        */
        public static int GetUserId(ApplicationUser sender)
        {
            return DBFacade.GetUserId(sender);
        }

        /**
        * forwarding request to send message, returning message confirnation
        */
        public static CreateMessageResponseVM SendNewMessage(NewMessageVM vm, ApplicationUser sender)
        {
            CreateMessageResponseVM response = new BusinessLogic().SendNewMessage(vm, sender);
            return response;
        }
        /**
        * forwarding request to get inboxInfo
        */
        public static ReadInboxVM GetInboxInfo(ApplicationUser user)
        {
            return new BusinessLogic().GetConversations(user);
        }

        /**
        * forwarding request to get inboxInfo
        */
        public static ReadInboxVM GetUserInboxStatistics(ApplicationUser user)
        {
            return new BusinessLogic().GetUserInboxStatistics(user);
        }

        /**
        * forwarding request to mark message as deleted.
        */
        internal static bool DeleteMessage(DeleteMessageVM vm, ApplicationUser user)
        {
            return new BusinessLogic().DeleteMessage(vm.id, user);
        }
    }
}
