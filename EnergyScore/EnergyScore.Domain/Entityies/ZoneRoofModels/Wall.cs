
using EnergyScore.Domain.Entityies.AddressModels;

namespace EnergyScore.Domain.Entityies.ZoneRoofModels
{
    public class Wall
    {
        public Guid Id { get; set; }
        public string WallName { get; set; }
        public string AtticWallType { get; set; }
        public double Area { get; set; }
        public Guid AtticId { get; set; }
        public Attic Attics { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
