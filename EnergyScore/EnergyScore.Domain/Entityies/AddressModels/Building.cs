
using EnergyScore.Domain.Entityies.AboutModels;
using EnergyScore.Domain.Entityies.ZoneFloorModels;
using EnergyScore.Domain.Entityies.ZoneRoofModels;
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies.AddressModels
{
    public class Building
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? AboutId { get; set; } = null;
        public About? Abouts { get; set; } = null;
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public Guid? ZoneFloorId { get; set; } = null;
        public ZoneFloor? ZoneFloors { get; set; } = null;
        public Guid? ZoneRoofId { get; set; } = null;
        public ZoneRoof? ZoneRoofs { get; set; } = null;
    }
}
