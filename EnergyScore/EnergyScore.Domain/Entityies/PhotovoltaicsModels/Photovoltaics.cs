

namespace EnergyScore.Domain.Entityies.PhotovoltaicsModels
{
    public class Photovoltaics
    {
        public Guid Id { get; set; }
        public ICollection<PVSystem> PVSystems { get; set; }
    }
}
