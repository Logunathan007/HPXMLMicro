using EnergyScore.Application.Mappers.DTOS.CommonDTOS;


namespace EnergyScore.Application.Mappers.DTOS.DistributionSystemDTOS
{
    public class DuctDTO
    {
        public Guid Id { get; set; }
        public double DuctInsulationRValue { get; set; }
        public double DuctInsulationThickness { get; set; }
        public string DuctInsulationMaterial { get; set; }
        public InsulationMaterialDynamicOptionsDTO DuctInsulationMaterialDynamicOptions { get; set; }
        public string DuctLocation { get; set; }
        public double FractionDuctArea { get; set; }
    }
}
