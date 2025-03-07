﻿
using EnergyScore.Domain.Entityies.AddressModels;

namespace EnergyScore.Domain.Entityies.ZoneRoofModels
{
    public class Skylight
    {
        public Guid Id { get; set; }
        public double Area { get; set; }
        public double SHGC { get; set; }
        public double UFactor { get; set; }
        public Guid? RoofId { get; set; }
        public Roof? Roof { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
