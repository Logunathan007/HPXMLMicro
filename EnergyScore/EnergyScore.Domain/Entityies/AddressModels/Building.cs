using EnergyScore.Domain.Entityies.AboutModels;
using EnergyScore.Domain.Entityies.ZoneFloorModels;
using EnergyScore.Domain.Entityies.ZoneRoofModels;
using EnergyScore.Domain.Entityies.ZoneWallModels;
using System.ComponentModel.DataAnnotations;
using EnergyScore.Domain.Entityies.DistributionSystemModels;
using EnergyScore.Domain.Entityies.HVACPlantModels;
using EnergyScore.Domain.Entityies.WaterHeatingModels;
using EnergyScore.Domain.Entityies.PhotovoltaicsModels;

namespace EnergyScore.Domain.Entityies.AddressModels
{
    public class Building
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? AboutId { get; set; } 
        public About? Abouts { get; set; } 
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public Guid? ZoneFloorId { get; set; } 
        public ZoneFloor? ZoneFloors { get; set; } 
        public Guid? ZoneRoofId { get; set; } 
        public ZoneRoof? ZoneRoofs { get; set; }
        public Guid? ZoneWallId { get; set; }
        public ZoneWall? ZoneWall { get; set; }
        public Guid? DistributionSystemsId { get; set; }
        public DistributionSystems? DistributionSystems { get; set; }   
        public Guid? HVACPlantId { get; set; }
        public HVACPlant? HVACPlant { get; set; }
        public Guid? WaterHeatingId { get; set; }
        public WaterHeating? WaterHeating { get; set; }
        public Guid? PhotovoltaicsId { get; set; }
        public Photovoltaics? Photovoltaics { get; set; }
        public ICollection<Slab>? Slab { get; set; }
        public ICollection<Foundation>? Foundation { get; set; }
        public ICollection<FoundationWall>? FoundationWall { get; set; }
        public ICollection<FrameFloor>? FrameFloor { get; set; }
        public ICollection<Attic>? Attic { get; set; }
        public ICollection<Roof>? Roof { get; set; }
        public ICollection<Wall>? Wall { get; set; }
        public ICollection<Window>? Windows { get; set; }
        public ICollection<Skylight>? Skylights { get; set; }
        public ICollection<DistributionSystem>? DistributionSystem { get; set; }
        public ICollection<HeatingSystem>? HeatingSystems { get; set; }
        public ICollection<CoolingSystem>? CoolingSystems { get; set; }
        public ICollection<HeatPump>? HeatPumps { get; set; }
        public ICollection<WaterHeatingSystem>? WaterHeatingSystems { get; set; }
        public ICollection<PVSystem>? PVSystems { get; set; }
    }
}
