
namespace EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS
{
    public class FoundationTypeDynamicOptionDTO
    {
        public Guid Id { get; set; }
        public bool? Finished { get; set; }
        public bool? Conditioned { get; set; }
        public bool? Vented { get; set; }
    }
}
