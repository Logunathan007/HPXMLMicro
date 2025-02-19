﻿using EnergyScore.Domain.Entityies.AddressModels;
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies.ZoneFloorModels
{
    public class Foundation
    {
        [Key]
        public Guid Id { get; set; }
        public string FoundationName { get; set; }
        public string FoundationType { get; set; }
        public FoundationTypeDynamicOption FoundationTypeDynamicOptions { get; set; }
        public ICollection<FoundationWall> FoundationWalls { get; set; }
        public ICollection<Slab> Slabs { get; set; }
        public ICollection<FrameFloor> FrameFloors { get; set; }
        public Guid ZoneFloorId { get; set; }
        public ZoneFloor ZoneFloor { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
