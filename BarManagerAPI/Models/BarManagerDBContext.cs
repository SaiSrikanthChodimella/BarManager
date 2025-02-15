using Microsoft.EntityFrameworkCore;

namespace BarManagerAPI.Models
{
    public class BarManagerDBContext(DbContextOptions<BarManagerDBContext> options) : DbContext(options)
    {
        public DbSet<MenuItem> MenuItem { get; set; }

        public DbSet<MenuCategory> MenuCategory { get; set; }

        public DbSet<TeamMembers> TeamMembers { get; set; }

        public DbSet<EventItems> EventItems { get; set; }   
    }
}
