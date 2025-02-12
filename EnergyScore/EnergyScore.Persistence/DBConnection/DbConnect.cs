using Microsoft.EntityFrameworkCore;
using EnergyScore.Domain.Entityies;
namespace EnergyScore.Persistence.DBConnection
{
    public class DbConnect:DbContext
    {
        public DbConnect(DbContextOptions<DbConnect> options) : base(options)
        {
            /*Database.EnsureCreated();*/
        }
        public DbSet<Address> Addresss { get; set; }
        public DbSet<AirInfiltrationMeasurement> AirInfiltrationMeasurements { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Building> Buildings { get; set; }
    }
}
/*

-- for migrations

Add-Migration makeConnection -Project EnergyScore.Persistence -StartupProject EnergyScore.Service
Update-Database -Project YourDataProject -StartupProject YourWebAPIProject
 
 */