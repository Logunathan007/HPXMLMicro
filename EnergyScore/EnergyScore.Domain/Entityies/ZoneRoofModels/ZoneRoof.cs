
namespace EnergyScore.Domain.Entityies.ZoneRoofModels
{
    public class ZoneRoof
    {
        public Guid Id { get; set; }
        public ICollection<Attic> Attics { get; set; }
    }
}
