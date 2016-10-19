using community.DBLayer;
using community.Models.DBModels;
using community.Models.BusinessModels;
using System.Collections.Generic;
using community.Models;
using System;
using community.Models.ViewModels;
using community.ListUtils;
using community.Models.ViewModels.ReadMessageViewModels;
using System.Linq;


namespace community.Business
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class BusinessLogic
    {   

        ////////////////////////////////////////////////////////////////////////
        //////////////////////////DELETE///////////////////////////////////////
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
        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        /**
        * forwarding request to insert group into db, returns groupBL
        */
        public GroupBL InsertGroup(GroupBL group) {
           return DBFacade.InsertGroup(group);                
        }

        /**
        * forwarding request get all the available destinations for a specific user.
        * returns list of DestinationBL
        */
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

        /**
        * request to get group mathcing ID, returns GroupBL
        */
        public GroupBL GroupsWithKey(int groupId)
        {
            return DBFacade.GetGroup(groupId);
        }

        /**
        * forwarding request get messages for specific user.
        * returns list of MessageBL
        */
        public List<MessageBL> GetUsersMessages(ApplicationUser user)
        {   
            return DBFacade.GetUsersMessages(user).Where(m => m.IsDeleted == false).ToList();;
        }

        /**
        * returns numberOfUnreadMessages for a user.
        */
        public int GetUsersUnreadMessagesCount(ApplicationUser user)
        {
            var msg = DBFacade.GetUsersMessages(user);
            
            return ListConverter.Reduce(msg,0,(count,m) => (m.IsRead == false) ? count + 1 : count );
        }
        /**
        * forwarding request to join group
        */
        internal bool JoinGroup(ApplicationUser user, int groupId)
        {
            return DBFacade.JoinGroup(user,groupId);
        }

        /**
        * forwarding request get messages from specific sender to specific user.
        * returns list of MessageBL
        */
        internal List<MessageBL> GetUsersMessagesWithSender(ApplicationUser user, int senderId)
        {
            List<MessageBL> msg =  DBFacade.GetUsersMessagesWithSender(user,senderId);
            return ListConverter.Filter(msg, m => m.IsDeleted == false);
        }

               /**
        * forwarding request get all groups
        * returns list of GroupBL 
        */
        public List<GroupBL> GetGroups(ApplicationUser user){
            var allGroups =  DBFacade.GetGroups();
            var joinedGroups = DBFacade.GetUserGroupDestinations(user);
            foreach(DestinationBL dest in joinedGroups) {
                GroupBL group = allGroups.Single(g => g.Id == dest.Id);
                if(group != null) {
                    group.HaveJoined = true;
                }
            }
            return allGroups;
        }

        
        /**
        * request message from db, returning viewModel only containing content + MessageId
        */
        public MessageBodyBL ReadMessageBody(MessageBodyBL msgbody,int reader) {
            var a = DBFacade.ReadMessage(reader,msgbody.Id);
            return new MessageBodyBL{Id = a.Id,Content = a.Content};
            
        }
        /**
        * attatches sender to message before forwarding request to post message to group, 
        */
        public void PostMessageToGroup(MessageBL msg,int groupId, ApplicationUser sender) 
        {   
            msg.Sender = sender;
            DBFacade.PostMessageToGroup(msg,groupId);
        }
        /**
        * Returns timeStamp of latest login
        */
        public DateTime GetLatestLoginTimeStamp(ApplicationUser user){
            DateTime latest = new DateTime();
            List<DateTime> timeStamps = DBFacade.GetLoginTimeStamps(user);
            
            foreach(DateTime dt in timeStamps){
                if (dt > latest){
                    System.Console.WriteLine( "TimeStamp: "+dt.ToString()+":is later than : "+latest.ToString());
                    latest = dt;
                }
            }
            return latest;
        }
        /**
        * Request to get userName, noOfLoginThisMonth, UnreadMessages, timeStamp of latest login
        * Returns home info 
        */
        public HomeVM GetHomeInfo(ApplicationUser user)
        {   
            HomeVM info = new HomeVM();
            info.UserName = user.UserName;
            info.noOfLoginThisMonth = DBFacade.GetNumberOfLogins(user);
            info.UnreadMessages = GetUsersUnreadMessagesCount(user);
            info.TimeStamp = GetLatestLoginTimeStamp(user).ToString();

            System.Console.WriteLine( "HomeInfo created and sending back: "+ info.ToString() );
            return info;

        }
        /**
        * Request to insert message to all recepiants 
        * Returns confirmation message 
        */
        internal CreateMessageResponseVM SendNewMessage(NewMessageVM vm, ApplicationUser sender)
        {

            List<MessageBL> sentMessages = new List<MessageBL>();
            //MessageBL tmpMsg = new MessageBL{Id = 0, Content = vm.textArea,IsRead = false,IsDeleted = false,SenderId = 42,Sender = sender};
            foreach(DestinationVM d in vm.destinations) {
                MessageBL tmpMsg = new MessageBL{Id = 0, Content = vm.textArea,Title = vm.title, IsRead = false,IsDeleted = false,Sender = sender};
                MessageBL msgSent;//= DBFacade.SendMessage(d.destinationId,tmpMsg,sender);
                if(d.isGroup) {
                    msgSent = DBFacade.PostMessageToGroup(tmpMsg,d.destinationId);
                }else {
                     msgSent = DBFacade.SendMessage(d.destinationId,tmpMsg,sender);
                }
                sentMessages.Add(msgSent);
            }
            
           return new CreateMessageResponseVM{destinations = vm.destinations,timeStamp = sentMessages[sentMessages.Count - 1].TimeStamp.ToString(), title = vm.title};
        }

        /**
        * Forwards the request to mark message as deleted 
        * returning boolean of result
        */
        internal bool DeleteMessage(int messageId, ApplicationUser user)
        {
            return DBFacade.DeleteMessage(messageId,user);
        }

        /**
        * getting unique conversations from DB and information about total-received/read/deleted-mesages 
        * returns ReadInboxVM filled with candy.
        */
        public ReadInboxVM GetConversations(ApplicationUser user) 

        {   
            List<InboxBL> inboxes = DBFacade.GetConversations(user);
            ReadInboxVM inbox = BusinessModelConverter.ConvertInboxListToInboxVM(inboxes);
            List<MessageBL> messages = new List<MessageBL>();
            messages = GetUsersMessages(user);

            inbox.totalReceivedMessages = messages.Count();
            inbox.totalReadMessages = messages.Where(x => x.IsRead == true).ToList().Count();
            inbox.totalDeletedMessages = messages.Where(x => x.IsDeleted == true).ToList().Count();

           return inbox;
        }
    }
}
