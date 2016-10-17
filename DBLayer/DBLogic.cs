

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
            System.Console.WriteLine("Group before: "+ group);
            ctx.Groups.Add(group);
            ctx.SaveChanges();
            System.Console.WriteLine("Group after: "+ group);
            
            return group;
        }

        public List<DestinationBL> GetUserGroupDestinations(ApplicationUser sender)
        {
            //ctx.Users.
            var user = ctx.Users.Include(m => m.Groups).Include(u => u.UserId).Single(u => u.Id == sender.Id);
            List<GroupDB> group = user.Groups;
            return ListUtils.ListConverter.Map(group,m => new DestinationBL { Id = m.Id, Name = m.Title, IsGroup = true });
        }

        public List<DestinationBL> GetUserDestinations(ApplicationUser sender)
        {
            //ctx.Users.
            var user = ctx.Users.Include(u => u.UserId).ToList();
            ListConverter.DoAction(user, u => System.Console.WriteLine("user id: " + u.UserId.Id));
            
            return ListConverter.Map(user,m => new DestinationBL { Id = m.UserId.Id, Name = m.UserName, IsGroup = false });
        }

        public List<MessageDB> GetUsersMessages(ApplicationUser user)
        {
            var dude = ctx.Users.Include(u => u.ReceivedMessages).Single(u => u.Id == user.Id);
            if(dude == null) {
                return new List<MessageDB>();
            }
            return dude.ReceivedMessages;
        }

        internal string SendMessage(int destinationId, MessageDB messageDB, ApplicationUser sender)
        {
            var targetUser = ctx.Users.Include(u => u.ReceivedMessages).Include(u => u.UserId).Single( u => u.UserId.Id == destinationId);
            var senderUser = ctx.Users.Include(u => u.SentMessages).Single( u => u.Id == sender.Id);
            //messageDB.Sender = senderUser;
            //messageDB.SenderId = senderUser.UserId.Id;
            senderUser.SentMessages.Add(messageDB);
            targetUser.ReceivedMessages.Add(messageDB);
            ctx.SaveChanges();
            return "this is the time";
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

        public MessageDB ReadMessage(int sender,int messageId) {
            var userId = sender;
            var user = ctx.Users.Include(u => u.UserId).Include(u => u.ReceivedMessages).Single(u => u.UserId.Id == userId);
            var msg = user.ReceivedMessages.Where(m => m.Id == messageId).Single();

            System.Console.WriteLine( "ReadMessage: msgdb:" + msg.ToString() );
            return msg;
        }

        public void PostMessageToGroup(MessageDB msg, int groupId)
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
        }
    }
}