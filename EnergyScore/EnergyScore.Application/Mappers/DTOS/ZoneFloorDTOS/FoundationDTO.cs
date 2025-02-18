namespace EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS
{
    public class FoundationDTO
    {
        public Guid Id { get; set; }
        public string FoundationName { get; set; }
        public string FoundationType { get; set; }
        public FoundationTypeDynamicOptionDTO FoundationTypeDynamicOptions { get; set; }
        public ICollection<FoundationWallDTO> FoundationWalls { get; set; }
        public ICollection<SlabDTO> Slabs { get; set; }
        public ICollection<FrameFloorDTO> FrameFloors { get; set; }
        public Guid BuildingId { get; set; }
    }
}
