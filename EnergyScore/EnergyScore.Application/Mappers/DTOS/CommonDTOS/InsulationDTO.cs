

namespace EnergyScore.Application.Mappers.DTOS.CommonDTOS
{
    public class InsulationDTO
    {
        public Guid Id { get; set; }
        public double AssemblyEffectiveRValue { get; set; }
        public ICollection<LayerDTO> Layers { get; set; }

    }
}
