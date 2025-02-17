
namespace EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS
{
    public class ZoneRoofDTO
    {
        public Guid Id { get; set; }
        public ICollection<AtticDTO> Attics { get; set; }
    }
}
