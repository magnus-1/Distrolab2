

using System.Linq;
using community.Data;
using community.Models.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace community.DBLayer {

    public class DBLogic {


        private ApplicationDbContext ctx = ApplicationDbContext.Create();
        public string GetEntries() {
            EntryDB a =  ctx.Entries.First();
            System.Console.WriteLine("-------  db sak" + a.ToString());
            return a.NewsItem;
        }

        public string EntriesWithKey(int key)
        {
            var entry = ctx.Entries.Where(c => c.Id == key).ToList();
            return entry.First().NewsItem;//First().NewsItem;
        }
        
        public void InsertEntry(EntryDB entry) {
            ctx.Entries.Add(entry);//.Entries.Add(entry);
            ctx.SaveChanges();
        }

        public void InsertGroup(GroupDB group) {
            ctx.Groups.Add(group);
            ctx.SaveChanges();
        }



        public GroupDB GetGroup(int groupId) {
            var group = ctx.Groups.Include(m => m.Messages).Single(p => p.Id == groupId);
            return group;
        }

        public void PostMessageToGroup(MessageDB msg,int groupId) 
        {
            var a = ctx.Groups.Where(c => c.Id == groupId);
            a.First().Messages.Add(msg);
            ctx.Messages.Add(msg);
            ctx.SaveChanges();
        }
    }
}