using EnergyScore.Domain.Entityies.AddressModels;
using EnergyScore.Domain.Entityies.ZoneRoofModels;

namespace EnergyScore.Domain.Entityies.ZoneWallModels
{
    public class Window
    {
        public Guid Id { get; set; }
        public double Area { get; set; }
        public double SHGC { get; set; }
        public double UFactor { get; set; }
        public string FrameType { get; set; }
        public FrameTypeDynamicOptions FrameTypeDynamicOptions { get; set; }
        public string GlassType { get; set; }
        public string GlassLayers { get; set; }
        public string GasFill { get; set; }
        public string Orientation { get; set; }
        public int Azimuth { get; set; }
        public Guid WallId { get; set; }
        public Wall Wall { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
