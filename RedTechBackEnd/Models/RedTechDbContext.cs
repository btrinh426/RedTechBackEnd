using Microsoft.EntityFrameworkCore;

namespace RedTechBackEnd.Models
{
    public class RedTechDbContext : DbContext
    {
        public RedTechDbContext(DbContextOptions option) : base(option)
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}
