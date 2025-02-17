

namespace EnergyScore.Domain.Entityies.ZoneRoofModels
{
    public class AtticTypeDynamicOption
    {
        public Guid Id { get; set; }
        public bool? Vented { get; set; } = null;
        public bool? Conditioned { get; set; } = null;
        public bool? CapeCod { get; set; } = null;
        public Guid AtticId { get; set; }
        public Attic Attics { get; set; }
    }
}