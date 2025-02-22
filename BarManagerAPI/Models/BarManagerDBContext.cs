using Microsoft.EntityFrameworkCore;

namespace BarManagerAPI.Models
{
    public class BarManagerDBContext(DbContextOptions<BarManagerDBContext> options) : DbContext(options)
    {
        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<MenuCategory> MenuCategories { get; set; }

        public DbSet<TeamMember> TeamMembers { get; set; }

        public DbSet<EventItem> EventItems { get; set; }   

        public DbSet<User> Users { get; set; }
    }
}
