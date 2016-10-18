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
        public GroupBL InsertGroup(GroupBL group) {
           return DBFacade.InsertGroup(group);                
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

        public List<MessageBL> GetUsersMessages(ApplicationUser user)
        {
            return DBFacade.GetUsersMessages(user);
        }

        public int GetUsersUnreadMessagesCount(ApplicationUser user)
        {
            var msg = DBFacade.GetUsersMessages(user);
            
            return ListConverter.Reduce(msg,0,(count,m) => (m.IsRead == false) ? count + 1 : count );
        }

        internal bool JoinGroup(ApplicationUser user, int groupId)
        {
            return DBFacade.JoinGroup(user,groupId);
        }

        internal List<MessageBL> GetUsersMessagesWithSender(ApplicationUser user, int senderId)
        {
            List<MessageBL> msg =  DBFacade.GetUsersMessagesWithSender(user,senderId);
            return ListConverter.Filter(msg, m => m.IsDeleted == false);
        }

        public List<GroupBL> GetGroups(){
            return DBFacade.GetGroups();
        
        }

        

        public MessageBodyBL ReadMessageBody(MessageBodyBL msgbody,int reader) {
            var a = DBFacade.ReadMessage(reader,msgbody.Id);
            return new MessageBodyBL{Id = a.Id,Content = a.Content};
            
        }

        public void PostMessageToGroup(MessageBL msg,int groupId, ApplicationUser sender) 
        {   
            msg.Sender = sender;
            DBFacade.PostMessageToGroup(msg,groupId);
        }

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
        internal CreateMessageResponseVM SendNewMessage(NewMessageVM vm, ApplicationUser sender)
        {
            string timeStamp = "none sent";
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

        internal bool DeleteMessage(int messageId, ApplicationUser user)
        {
            return DBFacade.DeleteMessage(messageId,user);
        }

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
