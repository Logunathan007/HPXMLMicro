using EnergyScore.Domain.Entityies.AboutModels;
using EnergyScore.Domain.Entityies.AddressModels;
using EnergyScore.Domain.Entityies.ZoneFloorModels;
using EnergyScore.Domain.Entityies.CommonModels;
using Microsoft.EntityFrameworkCore;
using EnergyScore.Domain.Entityies.ZoneRoofModels;
using EnergyScore.Domain.Entityies.ZoneWallModels;
using EnergyScore.Domain.Entityies.DistributionSystemModels;
using EnergyScore.Domain.Entityies.HVACPlantModels;
using EnergyScore.Domain.Entityies.WaterHeatingModels;
using EnergyScore.Domain.Entityies.PhotovoltaicsModels;


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
        public DbSet<Layer> Layers{ get; set; }
        public DbSet<ZoneFloor> ZoneFloors { get; set; }
        public DbSet<Attic> Attics { get; set; }
        public DbSet<Roof> Roofs { get; set; }
        public DbSet<Wall> Walls { get; set; }
        public DbSet<ZoneRoof> ZoneRoofs { get; set; }
        public DbSet<ZoneWall> ZoneWalls { get; set; }
        public DbSet<Skylight> Skylights { get; set; }
        public DbSet<AtticTypeDynamicOption> AtticTypeDynamicOptions { get; set; }
        public DbSet<FrameTypeDynamicOptions> FrameTypeDynamicOptions { get; set; }
        public DbSet<WallTypeDynamicOptions> WallsTypeDynamicOptions { get;set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<InsulationMaterialDynamicOptions> InsulationMaterialDynamicOptions {  get; set; }
        public DbSet<DistributionSystems> DistributionSystems { get; set; }
        public DbSet<Duct> Ducts { get; set; }  
        public DbSet<DistributionSystem> DistributionSystem { get; set; }
        public DbSet<CoolingSystem> CoolingSystems { get; set; }
        public DbSet<HeatingSystem> HeatingSystems { get; set; }
        public DbSet<HeatPump> HeatPumps { get; set; }
        public DbSet<HVACPlant> HVACPlants { get; set; }
        public DbSet<WaterHeating> WaterHeatings { get; set; }
        public DbSet<WaterHeatingSystem> WaterHeatingSystems { get; set; }
        public DbSet<Photovoltaics> Photovoltaics { get; set; }
        public DbSet<PVSystem> PVSystems { get; set; }
    }
}

/*

-- for migrations

Add-Migration makeConnection -Project EnergyScore.Persistence -StartupProject EnergyScore.Service
Update-Database -Project YourDataProject -StartupProject YourWebAPIProject

3fa85f64-5717-4562-b3fc-2c963f66afa6

 */