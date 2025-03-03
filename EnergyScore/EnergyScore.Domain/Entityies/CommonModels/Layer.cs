namespace EnergyScore.Domain.Entityies.CommonModels
{
    public class Layer
    {
        public Guid Id { get; set; }
        public double? NominalRValue { get; set; }
        public string? InstallationType { get; set; }
        public string? InsulationMaterial { get; set; }
        public InsulationMaterialDynamicOptions? InsulationMaterialDynamicOptions { get; set; }
    }
}
