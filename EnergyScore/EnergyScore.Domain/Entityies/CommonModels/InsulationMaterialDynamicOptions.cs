

using EnergyScore.Domain.Entityies.DistributionSystem;

namespace EnergyScore.Domain.Entityies.CommonModels
{
    public class InsulationMaterialDynamicOptions
    {
        public Guid Id { get; set; }
        public string? Batt {  get; set; }
        public string? LooseFill { get; set; }
        public string? Rigit { get; set; }
        public string? SprayFoam { get; set; }
        public Insulation? Insulation { get; set; }
        public Guid? InsulationId { get; set; }
        public Duct? Duct { get; set; }
        public Guid DuctId { get; set; }
    }
}
