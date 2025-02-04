
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies
{
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }   
        public int Zipcode { get; set; }
    }
}
