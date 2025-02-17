
namespace EnergyScore.Domain.Entityies.ZoneRoofModels
{
    public class Wall
    {
        public Guid Id { get; set; }
        public string WallName { get; set; }
        public string AtticWallType { get; set; }
        public Guid AtticId { get; set; }
        public Attic Attics { get; set; }
    }
}
