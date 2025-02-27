

namespace EnergyScore.Domain.Entityies.DistributionSystemModels
{
    public class DistributionSystems
    {
        public Guid Id { get; set; }
        public ICollection<DistributionSystem> DistributionSystem { get; set; }
    }
}
