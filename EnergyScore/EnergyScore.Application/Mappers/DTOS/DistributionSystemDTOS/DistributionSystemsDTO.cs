
namespace EnergyScore.Application.Mappers.DTOS.DistributionSystemDTOS
{
    public class DistributionSystemsDTO
    {
        public Guid Id { get; set; }    
        public ICollection<DistributionSystemDTO> DistributionSystem { get; set; }
    }
}
