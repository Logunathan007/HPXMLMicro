
using EnergyScore.Domain.Entityies.AddressModels;

namespace EnergyScore.Domain.Entityies.PhotovoltaicsModels
{
    public class PVSystem
    {
        public Guid Id { get; set; }
        public string PVSystemName { get; set; }
        public double MaxPowerOutput { get; set; }
        public double CollectorArea { get; set; }
        public int NumberOfPanels { get; set; }
        public int YearInverterManufactured { get; set; }
        public int YearModulesManufactured { get; set; }
        public double ArrayAzimuth { get; set; }
        public string ArrayOrientation { get; set; }
        public double ArrayTilt { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
        public Guid PhotovoltaicsId { get; set; }
        public Photovoltaics Photovoltaics { get; set; }
    }
}
