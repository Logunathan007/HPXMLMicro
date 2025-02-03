using Microsoft.EntityFrameworkCore;
using EnergyScore.Domain.Entityies;
using EnergyScore.Persistence.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnergyScore.Persistence.DBConnection
{
    public class DbConnect:DbContext
    {
        public DbConnect(DbContextOptions<DbConnect> options) : base(options)
        {
            
        }
        public DbSet<Address> Address { get; set; }
    }
}
/*


-- for migrations

Add - Migration InitialCreate - Project YourDataProject - StartupProject YourWebAPIProject
Update-Database -Project YourDataProject -StartupProject YourWebAPIProject*/