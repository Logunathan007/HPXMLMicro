using EnergyScore.Domain.Entityies.AboutModels;
using EnergyScore.Domain.Entityies.ZoneFloorModels;
using EnergyScore.Domain.Entityies.ZoneRoofModels;
using EnergyScore.Domain.Entityies.ZoneWallModels;
using System.ComponentModel.DataAnnotations;

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
        public ICollection<Slab>? Slab { get; set; }
        public ICollection<Foundation>? Foundation { get; set; }
        public ICollection<FoundationWall>? FoundationWall { get; set; }
        public ICollection<FrameFloor>? FrameFloor { get; set; }
        public ICollection<Attic>? Attic { get; set; }
        public ICollection<Roof>? Roof { get; set; }
        public ICollection<Wall>? Wall { get; set; }
    }
}
