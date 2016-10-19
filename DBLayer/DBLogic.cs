

using System.Linq;
using community.Data;
using community.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using community.Models;
using community.Models.BusinessModels;
using community.ListUtils;

namespace community.DBLayer
{

    public class DBLogic
    {

        private ApplicationDbContext ctx = ApplicationDbContext.Create();
 

        /**
        * returns integer of the number of logins this actual month
        */
        public int GetNumberOfLoginsThisMonth(ApplicationUser user){
            DateTime now = DateTime.Now;
            int numberOfLoginsThisMonth = 0;

            List<LoginDB> logins = new List<LoginDB>();
            logins = ctx.Logins.Where(i => i.UserId == user.Id).ToList();
            foreach(LoginDB log in logins){
                if  (log.TimeStamp.Month == now.Month){
                    numberOfLoginsThisMonth++;    
                }
            }

            return numberOfLoginsThisMonth;
        }
        
        /**
        * Returns a list of all the login timeStamps for a user. 
        */
        public List<DateTime> GetLoginTimeStamps(ApplicationUser user){
            List<DateTime> TimeStamps = new List<DateTime>(); 
            List<LoginDB> logins = new List<LoginDB>();
            logins = ctx.Logins.Where(i => i.UserId == user.Id).ToList();
            foreach(LoginDB log in logins){
                TimeStamps.Add(log.TimeStamp);
            }
            return TimeStamps;
        }

        /**
        * Inserts a group into database that other users vill be able to join 
        */
        public GroupDB InsertGroup(GroupDB group)
        {

            ctx.Groups.Add(group);
            ctx.SaveChanges();


            return group;
        }
        /**
        * returns list of all the groupdestinations a user can send messages to(aka groups that user has joined) 
        */
        public List<DestinationBL> GetUserGroupDestinations(ApplicationUser sender)
        {
            //ctx.Users.
            var user = ctx.Users.Include(m => m.Groups).Include(u => u.UserId).Single(u => u.Id == sender.Id);
            List<GroupDB> group = user.Groups;
            return ListUtils.ListConverter.Map(group, m => new DestinationBL { Id = m.Id, Name = m.Title, IsGroup = true });
        }
        /**
        * Insert group into users list of groups
        */
        internal bool JoinGroup(ApplicationUser user, int groupId)
        {
            var dude = ctx.Users.Include(m => m.Groups).Single(u => u.Id == user.Id);
            var group = ctx.Groups.Single(g => g.Id == groupId);
            if(group == null || dude == null) {
                return false;
            }
            dude.Groups.Add(group);
            ctx.SaveChanges();
            return true;
        }
        /**
        * Returns a list of all recipiants(Users) as valid destinations that messages can be sent to. 
        */
        public List<DestinationBL> GetUserDestinations(ApplicationUser sender)
        {
            //ctx.Users.
            var user = ctx.Users.Include(u => u.UserId).ToList();
            //ListConverter.DoAction(user, u => System.Console.WriteLine("user id: " + u.UserId.Id));

            return ListConverter.Map(user, m => new DestinationBL { Id = m.UserId.Id, Name = m.UserName, IsGroup = false });
        }
        /**
        * Marks a message as deleted
        */
        internal bool DeleteMessage(int messageId, ApplicationUser user)
        {
            var dude = ctx.Users.Include(u => u.ReceivedMessages).Single(u => u.Id == user.Id);
            if(dude == null) {
                return false;
            }
            var msg = dude.ReceivedMessages.Single(m => m.Id == messageId);
            if(msg == null) {
                return false;
            }
            
            msg.IsDeleted = true;
            ctx.SaveChanges();
            return true;
        }
        /**
        * Returns list of all messages from a specific sender
        */
        internal List<MessageDB> GetUsersMessagesWithSender(ApplicationUser user, int senderId)
        {
            var send = ctx.Users.Include(u => u.UserId).Single(u => u.UserId.Id == senderId);

            var dude = ctx.Users.Include(u => u.ReceivedMessages)
                    .ThenInclude(m => m.Sender)
                    .Single(u => u.Id == user.Id);
            if (dude == null)
            {
                return new List<MessageDB>();
            }
            List<MessageDB> result = ListConverter.Filter(dude.ReceivedMessages,m => m.Sender == send);

            return result;
        }
        /**
        * Returns a list of all received messages from a user
        */
        public List<MessageDB> GetUsersMessages(ApplicationUser user)
        {
            var dude = ctx.Users.Include(u => u.ReceivedMessages).ThenInclude(m => m.Sender).Single(u => u.Id == user.Id);
            if (dude == null)
            {
                return new List<MessageDB>();
            }
           
            return dude.ReceivedMessages;
        }
        /**
        * Inserts a message to the receivers receivedMessage list. 
        * Returns the message that was sent. 
        */
        internal MessageDB SendMessage(int destinationId, MessageDB messageDB, ApplicationUser sender)
        {


            var targetUser = ctx.Users.Include(u => u.ReceivedMessages).Include(u => u.UserId).Single(u => u.UserId.Id == destinationId);
            var senderUser = ctx.Users.Include(u => u.SentMessages).Single(u => u.Id == sender.Id);
            messageDB.TimeStamp = DateTime.Now;

            messageDB.Sender = sender;
            messageDB.SenderId = sender.Id;
            
            messageDB.Receiver = targetUser;
            messageDB.ReceiverId = targetUser.Id;

            senderUser.SentMessages.Add(messageDB);
            targetUser.ReceivedMessages.Add(messageDB);
            ctx.SaveChanges();

            return messageDB;
        }
        /**
        * Returns the Integer version of a userId, this is not the Id generated by IIdentity 2.0
        */
        public int GetUserId(ApplicationUser sender)
        {
            var user = ctx.Users.Include(u => u.UserId).Single(u => u.Id == sender.Id);
            return user.UserId.Id;
        }
        /**
        * Returns the group including the messages for this group that matches the groupID, 
        */
        public GroupDB GetGroup(int groupId)
        {
            var group = ctx.Groups.Include(m => m.Messages).ThenInclude(messages => messages.Sender).Single(p => p.Id == groupId);
            return group;
        }
        /**
        * Returns a list of all the groups in the community, not including it's messages.
        */
        public List<GroupDB> GetGroups()
        {
            var groups = ctx.Groups.ToList();
            return groups;
        }
        /**
        * Finds the message corresponding to the messageId.
        * Marks messages as read and returns the message.
        */
        public MessageDB ReadMessage(int sender, int messageId)
        {
            var userId = sender;
            var user = ctx.Users.Include(u => u.UserId).Include(u => u.ReceivedMessages).Single(u => u.UserId.Id == userId);
            var msg = user.ReceivedMessages.Where(m => m.Id == messageId).Single();

            msg.IsRead = true;
            ctx.SaveChanges();


            return msg;
        }
        /**
        * Adds a message to the groups messages. Specified by GroupId
        */
        public MessageDB PostMessageToGroup(MessageDB msg, int groupId)
        {

            var a = ctx.Groups.Include(m => m.Messages).Single(p => p.Id == groupId);
            a.Messages.Add(msg);
            ApplicationUser user = ctx.Users.Include(m => m.SentMessages).Single(p => p.Id == msg.Sender.Id);
            user.SentMessages.Add(msg);
            ctx.SaveChanges();


            // ApplicationUser user2 = ctx.Users.Include(m => m.SentMessages).Single(p => p.Id == msg.Sender.Id);
            // foreach (MessageDB m in user2.SentMessages){
            //     System.Console.WriteLine("Users sent messages  = "+ m);

            // }
            return msg;
        }



        /**
        *
        *
        */
        public class UserSender
        {
            public ApplicationUser Sender { get; set; }
            public int Count { get; set; }
        }
        /**
        *
        *
        */
        public List<UserSender> GetInboxForUser(ApplicationUser user)
        {
            var dude = ctx.Users.Include(u => u.ReceivedMessages).ThenInclude(m => m.Sender).Single(u => u.Id == user.Id);
            if (dude == null)
            {
                return new List<UserSender>();
            }

            List<MessageDB> rcvMsg = dude.ReceivedMessages;
            List<UserSender> uniqSender = new List<UserSender>();
            int count = 0;
            while (rcvMsg.Count > 0)
            {
                ApplicationUser sender = rcvMsg.First().Sender;
                rcvMsg = ListConverter.Filter(rcvMsg, m =>
                {
                    if(m.Sender == sender) {
                        count++;
                        return false;
                    }
                    return true;
                });
                uniqSender.Add(new UserSender{Sender = sender,Count = count});
            }
            return uniqSender;
        }

        /**
        * Goes thru all of the received messages to find heach unique sender of messages.
        * Returns List of all unique conversations between users.
        */
        public List<InboxDB> GetConversations(ApplicationUser user)
        {

            List<ApplicationUser> uniqueConversations = new List<ApplicationUser>();
            List<MessageDB> messages = new List<MessageDB>();
            List<InboxDB> InboxDBs = new List<InboxDB>();

            var currentUser = ctx.Users.Include(u => u.ReceivedMessages).ThenInclude(rm => rm.Sender).ThenInclude(u => u.UserId).Single(u => u.Id == user.Id);
            messages = currentUser.ReceivedMessages.Where(r => r.IsDeleted == false).ToList();

          
            foreach (MessageDB m in messages)
            {
                if (!uniqueConversations.Exists(p => p.Id == m.Sender.Id))
                {

                    uniqueConversations.Add(m.Sender);
                    //var senderUser = ctx.Users.Include(u => u.UserId).Single(i => i.m.Sender.Id);
                    int unreadMessagesFromSender = messages.Where(
                                                            x => x.SenderId == m.Sender.Id &&
                                                            x.IsRead == false &&
                                                            x.IsDeleted == false).ToList().Count();

                    InboxDB Inbox = new InboxDB
                    {
                        UserId = m.Sender.UserId.Id,
                        UnreadMessages = unreadMessagesFromSender,
                        Sender = m.Sender
                    };

                    InboxDBs.Add(Inbox);
                }
            }


            return InboxDBs;

        }
    }
}