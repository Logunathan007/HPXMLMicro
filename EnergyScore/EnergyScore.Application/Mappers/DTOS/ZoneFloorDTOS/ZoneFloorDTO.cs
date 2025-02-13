
namespace EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS
{
    public class ZoneFloorDTO
    {
        public Guid Id { get; set; }
        public ICollection<FoundationDTO> Foundations { get; set; }
    }
}
