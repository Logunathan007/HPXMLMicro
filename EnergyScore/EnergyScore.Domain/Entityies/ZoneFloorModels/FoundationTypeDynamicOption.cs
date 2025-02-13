
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies.ZoneFloorModels
{
    public class FoundationTypeDynamicOption
    {
        [Key]
        public Guid Id { get; set; }
        public bool? Finished { get; set; }
        public bool? Conditioned { get; set; }
        public bool? Vented { get; set; }
        public Guid FoundationId { get; set; }
        public Foundation Foundations{ get; set; }
    }
}
