

namespace EnergyScore.Application.Mappers.DTOS.CommonDTOS
{
    public class InsulationMaterialDynamicOptionsDTO
    {
        public Guid Id { get; set; }
        public string? Batt {  get; set; }
        public string? LooseFill { get; set; }
        public string? Rigid { get; set; }
        public string? SprayFoam { get; set; }
        public LayerDTO? Layer { get; set; }
        public Guid? LayerId { get; set; }
    }
}
