using Microsoft.EntityFrameworkCore;

namespace EnergyScore.Persistence.DBConnection
{
    public class DbConnect:DbContext
    {
        public DbConnect(DbContextOptions<DbConnect> options) : base(options)
        {
            
        }
        public DbSet<Domain.Entityies.Address> Address { get; set; }
    }
}
