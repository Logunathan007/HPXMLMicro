
namespace EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS
{
    public class WallDTO
    {
        public Guid Id { get; set; }
        public string WallName { get; set; }
        public double Area { get; set; }
        public string AtticWallType { get; set; }
        public Guid BuildingId { get; set; }
    }
}
