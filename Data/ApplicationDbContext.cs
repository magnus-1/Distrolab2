using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using community.Models;
using community.Models.DBModels;


namespace community.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlite("Filename=./community.db");
            return new ApplicationDbContext(options.Options);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasOne(p => p.UserId).WithOne().IsRequired();
            builder.Entity<GroupDB>()
                .HasMany(p => p.Messages)
                .WithOne();
            builder.Entity<MessageDB>()
                .HasOne(p => p.Sender)
                .WithMany(a => a.SentMessages);
            builder.Entity<MessageDB>()  
                .HasOne(r => r.Receiver)
                .WithMany(b => b.ReceivedMessages);
            builder.Entity<LoginDB>()  
                .HasOne(r => r.User);

            // to simulate many to meny i core
            builder.Entity<GroupMemberDB>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.GroupMembers)
                .HasForeignKey(gm => gm.GroupId);

            builder.Entity<GroupMemberDB>()
                .HasOne(gm => gm.User)
                .WithMany(u => u.GroupMembership)
                .HasForeignKey(gm => gm.UserId);

        }

        public DbSet<GroupDB> Groups { get; set; }
        public DbSet<GroupMemberDB> GroupMembership { get; set; }
        public DbSet<MessageDB> Messages { get; set; }
        public DbSet<LoginDB> Logins {get;set;}

        public override string ToString(){
            return "Detta ar en instans av AppDbContext!";
        }

        
        //public DbSet<ApplicationUser> Users {get;set;}
    }

}