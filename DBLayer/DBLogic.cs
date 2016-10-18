

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
        public string GetEntries()
        {
            EntryDB a = ctx.Entries.First();
            System.Console.WriteLine("-------  db sak" + a.ToString());
            return a.NewsItem;
        }

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
            System.Console.WriteLine( "NuberOfLoginsThisMonth: "+ numberOfLoginsThisMonth );
            return numberOfLoginsThisMonth;
        }

        public string EntriesWithKey(int key)
        {
            var entry = ctx.Entries.Where(c => c.Id == key).ToList();
            return entry.First().NewsItem;//First().NewsItem;
        }

        public void InsertEntry(EntryDB entry)
        {
            ctx.Entries.Add(entry);//.Entries.Add(entry);
            ctx.SaveChanges();
        }

        public GroupDB InsertGroup(GroupDB group)
        {
            System.Console.WriteLine("Group before: " + group);
            ctx.Groups.Add(group);
            ctx.SaveChanges();
            System.Console.WriteLine("Group after: " + group);

            return group;
        }

        public List<DestinationBL> GetUserGroupDestinations(ApplicationUser sender)
        {
            //ctx.Users.
            var user = ctx.Users.Include(m => m.Groups).Include(u => u.UserId).Single(u => u.Id == sender.Id);
            List<GroupDB> group = user.Groups;
            return ListUtils.ListConverter.Map(group, m => new DestinationBL { Id = m.Id, Name = m.Title, IsGroup = true });
        }

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

        public List<DestinationBL> GetUserDestinations(ApplicationUser sender)
        {
            //ctx.Users.
            var user = ctx.Users.Include(u => u.UserId).ToList();
            ListConverter.DoAction(user, u => System.Console.WriteLine("user id: " + u.UserId.Id));

            return ListConverter.Map(user, m => new DestinationBL { Id = m.UserId.Id, Name = m.UserName, IsGroup = false });
        }

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
            foreach (MessageDB m in result)
            {
                System.Console.WriteLine("GetUsersMessagesWithSender: msg from db: " + m.ToString());
            }

            return result;
        }

        public List<MessageDB> GetUsersMessages(ApplicationUser user)
        {
            var dude = ctx.Users.Include(u => u.ReceivedMessages).ThenInclude(m => m.Sender).Single(u => u.Id == user.Id);
            if (dude == null)
            {
                return new List<MessageDB>();
            }
            foreach (MessageDB m in dude.ReceivedMessages)
            {
                System.Console.WriteLine("Fetching msg from db: " + m.ToString());
            }

            return dude.ReceivedMessages;
        }

        internal MessageDB SendMessage(int destinationId, MessageDB messageDB, ApplicationUser sender)
        {
            System.Console.WriteLine("Inserting message to db: " + messageDB.ToString());

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
            System.Console.WriteLine("After inserting message to db: " + messageDB.ToString());
            return messageDB;
        }

        public int GetUserId(ApplicationUser sender)
        {
            var user = ctx.Users.Include(u => u.UserId).Single(u => u.Id == sender.Id);
            return user.UserId.Id;
        }

        public GroupDB GetGroup(int groupId)
        {
            var group = ctx.Groups.Include(m => m.Messages).ThenInclude(messages => messages.Sender).Single(p => p.Id == groupId);
            return group;
        }
        public List<GroupDB> GetGroups()
        {
            var groups = ctx.Groups.ToList();
            return groups;
        }

        public MessageDB ReadMessage(int sender, int messageId)
        {
            var userId = sender;
            var user = ctx.Users.Include(u => u.UserId).Include(u => u.ReceivedMessages).Single(u => u.UserId.Id == userId);
            var msg = user.ReceivedMessages.Where(m => m.Id == messageId).Single();

            msg.IsRead = true;
            ctx.SaveChanges();

            System.Console.WriteLine("ReadMessage: msgdb:" + msg.ToString());
            return msg;
        }

        public MessageDB PostMessageToGroup(MessageDB msg, int groupId)
        {
            System.Console.WriteLine("Message to be handled in db layer = " + msg.ToString());
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




        public class UserSender
        {
            public ApplicationUser Sender { get; set; }
            public int Count { get; set; }
        }

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


        public List<InboxDB> GetConversations(ApplicationUser user)
        {

            List<ApplicationUser> uniqueConversations = new List<ApplicationUser>();
            List<InboxDB> InboxDBs = new List<InboxDB>();

            var currentUser = ctx.Users.Include(u => u.ReceivedMessages).ThenInclude(rm => rm.Sender).ThenInclude(u => u.UserId).Single(u => u.Id == user.Id);
            System.Console.WriteLine("DBLogic:GetConversations:CurrnetUser = " + currentUser.ToString());
            System.Console.WriteLine("DBLogic:GetConversations:uniqueConversations count before = " + uniqueConversations.Count());

            foreach (MessageDB m in currentUser.ReceivedMessages)
            {
                if (!uniqueConversations.Exists(p => p.Id == m.Sender.Id))
                {
                    System.Console.WriteLine("DBLogic:GetConversations:Unique user added: " + m.Sender.UserName);
                    uniqueConversations.Add(m.Sender);
                    //var senderUser = ctx.Users.Include(u => u.UserId).Single(i => i.m.Sender.Id);
                    int unreadMessagesFromSender = currentUser.ReceivedMessages.Where(
                                                            x => x.SenderId == m.Sender.Id &&
                                                            x.IsRead == false &&
                                                            x.IsDeleted == false).ToList().Count();
                    InboxDB Inbox = new InboxDB
                    {
                        UserId = m.Sender.UserId.Id,
                        UnreadMessages = unreadMessagesFromSender,
                        Sender = m.Sender
                    };
                    System.Console.WriteLine("DBLogic:GetConversations:Inbox created: " + Inbox);
                    InboxDBs.Add(Inbox);
                }
            }

            System.Console.WriteLine("DBLogic:GetConversations:uniqueConversations count after = " + uniqueConversations.Count() + ", inboxes created = " + InboxDBs.Count());
            return InboxDBs;

        }
    }
}