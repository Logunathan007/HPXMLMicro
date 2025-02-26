
using EnergyScore.Domain.Entityies.AddressModels;

namespace EnergyScore.Domain.Entityies.DistributionSystem
{
    public class DistributionSystem
    {
        public Guid Id { get; set; }
        public string DistributionSystemName { get; set; }
        public string LeakinessObservedVisualInspection { get; set; }
        public bool DuctSystemSealed { get; set; }
        public string? DuctType { get; set; }
        public string Units { get; set; }
        public double Value { get; set; }
        public string TotalOrToOutside { get; set; }
        public ICollection<Duct> Ducts { get; set; }
        public Guid DistributionSystemsId { get; set; }
        public DistributionSystems DistributionSystems { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
