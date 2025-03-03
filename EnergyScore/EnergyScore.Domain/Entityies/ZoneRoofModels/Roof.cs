

using EnergyScore.Domain.Entityies.AddressModels;
using EnergyScore.Domain.Entityies.CommonModels;

namespace EnergyScore.Domain.Entityies.ZoneRoofModels
{
    public class Roof
    {
        public Guid Id { get; set; }
        public string RoofName { get; set; }
        public string RoofType { get; set; }
        public string RoofColor { get; set; }
        public double Area { get; set; }
        public bool RadiantBarrier { get; set; }
        public double SolarAbsorptance { get; set; }
        public ICollection<Insulation> Insulations { get; set; }
        public ICollection<Skylight> Skylights { get; set; }
        public Guid AtticId { get; set; }
        public Attic Attics { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
