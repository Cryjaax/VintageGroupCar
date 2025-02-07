using VintageGroupCar.Models;
using Microsoft.EntityFrameworkCore;

namespace VintageGroupCar.Dal
{
    public class AppDbContext : DbContext
    {
        public DbSet<Raduno> Raduni { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
