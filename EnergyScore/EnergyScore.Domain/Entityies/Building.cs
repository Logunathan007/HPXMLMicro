
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies
{
    public class Building
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AboutId { get; set; }
        public Guid AddressId { get; set; }
    }
}
