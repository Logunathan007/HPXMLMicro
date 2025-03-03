
namespace EnergyScore.Application.Mappers.DTOS.CommonDTOS
{
    public class LayerDTO
    {
        public Guid Id { get; set; }
        public double? NominalRValue { get; set; }
        public string? InstallationType { get; set; }
        public string? InsulationMaterial { get; set; }
        public InsulationMaterialDynamicOptionsDTO? InsulationMaterialDynamicOptions { get; set; }
    }
}
