

using EnergyScore.Domain.Entityies.ZoneFloorModels;

namespace EnergyScore.Domain.Entityies.ZoneRoofModels
{
    public class Attic
    {
        public Guid Id { get; set; }
        public string AtticName { get; set; }
        public string AtticType { get; set; }
        public AtticTypeDynamicOption AtticTypeDynamicOptions { get; set; }
        public ICollection<Roof> Roofs { get; set; }
        public ICollection<Wall> Walls { get; set; }
        public ICollection<FrameFloor> FrameFloors { get; set; }
        public Guid ZoneRoofId { get; set; }
        public ZoneRoof ZoneRoof { get; set; }
    }
}
