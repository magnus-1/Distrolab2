

using System.Linq;
using community.Data;
using community.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using community.Models;
using community.Models.BusinessModels;

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

        public void InsertGroup(GroupDB group)
        {
            ctx.Groups.Add(group);
            ctx.SaveChanges();
        }

        public List<DestinationBL> GetUserGroupDestinations(ApplicationUser sender)
        {
            //ctx.Users.
            var user = ctx.Users.Include(m => m.Groups).Single(u => u.Id == sender.Id);
            List<GroupDB> group = user.Groups;
            return ListUtils.ListConverter.Map(group,m => new DestinationBL { Id = m.Id, Name = m.Title, IsGroup = true });
        }

        public List<DestinationBL> GetUserDestinations(ApplicationUser sender)
        {
            //ctx.Users.
            var user = ctx.Users.ToList();
            ListUtils.ListConverter.Filter(user, u => {System.Console.WriteLine("user id: " + u.Id);return true;});

            return ListUtils.ListConverter.Map(user,m => new DestinationBL { Id = int.Parse(m.Id), Name = m.UserName, IsGroup = false });
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