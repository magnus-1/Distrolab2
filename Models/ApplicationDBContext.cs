using Microsoft.EntityFrameworkCore;

namespace Distrolab2.Models
{
    public class ApplicationDBContext : DbContext{
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder oprionsBuilder){
            oprionsBuilder.UseSqlite("Filename=.webNumberTwo.db");
        }
    }
}