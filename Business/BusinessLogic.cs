using community.DBLayer;
using community.Models.DBModels;
using community.Models.BusinessModels;
using System.Collections.Generic;
using community.Models;
using System;
using community.Models.ViewModels;

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

        internal CreateMessageResponseVM SendNewMessage(NewMessageVM vm, ApplicationUser sender)
        {
            string timeStamp = "none sent";
            List<MessageBL> sentMessages = new List<MessageBL>();
            //MessageBL tmpMsg = new MessageBL{Id = 0, Content = vm.textArea,IsRead = false,IsDeleted = false,SenderId = 42,Sender = sender};
            foreach(DestinationVM d in vm.destinations) {
                MessageBL tmpMsg = new MessageBL{Id = 0, Content = vm.textArea,Title = vm.title, IsRead = false,IsDeleted = false,Sender = sender};
                sentMessages.Add(DBFacade.SendMessage(d.destinationId,tmpMsg,sender));
            }
            
           return new CreateMessageResponseVM{destinations = vm.destinations,timeStamp = timeStamp, title = vm.title};
        }
        public void GetConversations(ApplicationUser user) 
        {   
            DBFacade.GetConversations(user);
        }
    }
}
