using Microsoft.EntityFrameworkCore;
using EnergyScore.Domain.Entityies;
namespace EnergyScore.Persistence.DBConnection
{
    public class DbConnect:DbContext
    {
        public DbConnect(DbContextOptions<DbConnect> options) : base(options)
        {
            
        }
        public DbSet<Address> Address { get; set; }
        public DbSet<About> About { get; set; }
    }
}
/*

-- for migrations

Add-Migration makeConnection -Project EnergyScore.Persistence -StartupProject EnergyScore.Service
Update-Database -Project YourDataProject -StartupProject YourWebAPIProject
 
 */