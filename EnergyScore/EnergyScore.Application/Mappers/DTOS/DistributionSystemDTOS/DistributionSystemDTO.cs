

namespace EnergyScore.Application.Mappers.DTOS.DistributionSystemDTOS
{
    public class DistributionSystemDTO
    {
        public Guid Id { get; set; }
        public string DistributionSystemName { get; set; }
        public string LeakinessObservedVisualInspection { get; set; }
        public bool DuctSystemSealed { get; set; }
        public string? DuctType { get; set; }
        public string Units { get; set; }
        public double Value { get; set; }
        public string TotalOrToOutside { get; set; }
        public ICollection<DuctDTO> Ducts { get; set; }
        public Guid BuildingId { get; set; }
    }
}
