using EnergyScore.Application.Mappers.DTOS.CommonDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS
{
    public class FoundationWallDTO
    {
        public Guid Id { get; set; }
        public string FoundationWallName { get; set; }
        public double Area { get; set; }
        public ICollection<InsulationDTO> Insulations { get; set; }
        public Guid BuildingId { get; set; }
    }
}
