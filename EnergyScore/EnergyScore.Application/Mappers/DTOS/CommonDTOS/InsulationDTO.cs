using EnergyScore.Domain.Entityies.CommonModels;

namespace EnergyScore.Application.Mappers.DTOS.CommonDTOS
{
    public class InsulationDTO
    {
        public Guid Id { get; set; }
        public double NominalRValue { get; set; }
        public double AssemblyEffectiveRValue { get; set; }
        public string? InstallationType { get; set; }
        public string? InsulationMaterial { get; set; }
        public InsulationMaterialDynamicOptionsDTO? InsulationMaterialDynamicOptions { get; set; }
    }
}
