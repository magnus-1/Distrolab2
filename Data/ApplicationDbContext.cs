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
            builder.Entity<GroupDB>()
                .HasMany(p => p.Messages)
                .WithOne();
            builder.Entity<MessageDB>()
                .HasOne(p => p.Sender)
                .WithMany(a => a.SentMessages);
            builder.Entity<ApplicationUser>()
                .HasMany(p => p.SentMessages)
                .WithOne(a => a.Sender);
            // builder.Entity<ApplicationUser>()
            //     .HasMany(p => p.ReceivedMessages)
            //     .WithOne(a => a.Sender);



            // TODO: builder.Entity<outstuff>.hasOne(k => till vad) . hasOneToMany

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<EntryDB> Entries { get; set; }
        public DbSet<GroupDB> Groups { get; set; }
        public DbSet<MessageDB> Messages { get; set; }
    }

}
