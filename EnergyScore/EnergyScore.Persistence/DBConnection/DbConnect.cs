using EnergyScore.Domain.Entityies.AboutModels;
using EnergyScore.Domain.Entityies.AddressModels;
using EnergyScore.Domain.Entityies.ZoneFloorModels;
using EnergyScore.Domain.Entityies.CommonModels;
using Microsoft.EntityFrameworkCore;
using EnergyScore.Domain.Entityies.ZoneRoofModels;
using System;


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
        public DbSet<Foundation> Foundations  { get; set; }
        public DbSet<FoundationTypeDynamicOption> FoundationTypeDynamicOptions { get; set; }
        public DbSet<FoundationWall> FoundationWalls { get; set; }
        public DbSet<FrameFloor> FrameFloors { get; set; }
        public DbSet<Slab> Slabs { get; set; }
        public DbSet<Insulation> Insulations{ get; set; }
        public DbSet<PerimeterInsulation> PerimeterInsulations{ get; set; }
        public DbSet<ZoneFloor> ZoneFloors { get; set; }
        public DbSet<Attic> Attics { get; set; }
        public DbSet<Roof> Roofs { get; set; }
        public DbSet<Wall> Walls { get; set; }
        public DbSet<ZoneRoof> ZoneRoofs { get; set; }
        public DbSet<Skylight> Skylights { get; set; }
        public DbSet<AtticTypeDynamicOption> AtticTypeDynamicOptions { get; set; }
    }
}

/*

-- for migrations

Add-Migration makeConnection -Project EnergyScore.Persistence -StartupProject EnergyScore.Service
Update-Database -Project YourDataProject -StartupProject YourWebAPIProject

3fa85f64-5717-4562-b3fc-2c963f66afa6

 */