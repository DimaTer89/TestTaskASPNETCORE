using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<LinkPhoto> Links { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
