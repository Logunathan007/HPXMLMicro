

namespace EnergyScore.Domain.Entityies.DistributionSystem
{
    public class DistributionSystems
    {
        public Guid Id { get; set; }
        public ICollection<DistributionSystem> DistributionSystem { get; set; }
    }
}
