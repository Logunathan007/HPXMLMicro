

namespace EnergyScore.Application.Mappers.DTOS.PhotovoltaicsDTOS
{
    public class PhotovoltaicsDTO
    {
        public Guid Id { get; set; }
        public ICollection<PVSystemDTO> PVSystems { get; set; }
    }
}
