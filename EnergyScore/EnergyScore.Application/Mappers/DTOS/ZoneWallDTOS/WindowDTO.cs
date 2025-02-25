
namespace EnergyScore.Application.Mappers.DTOS.ZoneWallDTOS
{
    public class WindowDTO
    {
        public Guid Id { get; set; }
        public double Area { get; set; }
        public double SHGC { get; set; }
        public double UFactor { get; set; }
        public string FrameType { get; set; }
        public FrameTypeDynamicOptionsDTO FrameTypeDynamicOptions { get; set; }
        public string GlassType { get; set; }
        public string GlassLayers { get; set; }
        public string GasFill { get; set; }
        public string Orientation { get; set; }
        public int Azimuth { get; set; }
        public Guid BuildingId { get; set; }
    }
}
