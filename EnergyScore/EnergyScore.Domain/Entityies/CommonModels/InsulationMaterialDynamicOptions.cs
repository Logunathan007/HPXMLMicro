

using EnergyScore.Domain.Entityies.DistributionSystemModels;

namespace EnergyScore.Domain.Entityies.CommonModels
{
    public class InsulationMaterialDynamicOptions
    {
        public Guid Id { get; set; }
        public string? Batt {  get; set; }
        public string? LooseFill { get; set; }
        public string? Rigid { get; set; }
        public string? SprayFoam { get; set; }
        public Layer? Layer { get; set; }
        public Guid? LayerId { get; set; }
    }
}
