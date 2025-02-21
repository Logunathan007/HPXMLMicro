using EnergyScore.Domain.Entityies.ZoneRoofModels;

namespace EnergyScore.Domain.Entityies.ZoneWallModels
{
    public class ZoneWall
    {
        public Guid Id { get; set; }
        public ICollection<Wall> Walls { get; set; }
    }
}
