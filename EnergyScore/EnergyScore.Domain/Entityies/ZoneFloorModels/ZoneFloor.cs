

namespace EnergyScore.Domain.Entityies.ZoneFloorModels
{
    public class ZoneFloor
    {
        public Guid Id { get; set; }
        public ICollection<Foundation> Foundations { get; set; }
    }
}
