
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies.CommonModels
{
    public class Insulation
    {
        [Key]
        public Guid Id { get; set; }
        public double NominalRValue { get; set; }
        public double AssemblyEffectiveRValue { get; set; }
        public string? InstallationType { get; set; }
        public string? InsulationMaterial { get; set; }
        public InsulationMaterialDynamicOptions? InsulationMaterialDynamicOptions { get; set; }
    }
}
