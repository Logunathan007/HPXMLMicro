

namespace EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS
{
    public class AtticTypeDynamicOptionDTO
    {
        public Guid Id { get; set; }
        public bool? Vented { get; set; } = null;
        public bool? Conditioned { get; set; } = null;
        public bool? CapeCod { get; set; } = null;
    }
}