using Microsoft.EntityFrameworkCore;

namespace BarManagerAPI.Models
{
    public class BarManagerDBContext(DbContextOptions<BarManagerDBContext> options) : DbContext(options)
    {
        public DbSet<MenuItem> MenuItem { get; set; }
    }
}
