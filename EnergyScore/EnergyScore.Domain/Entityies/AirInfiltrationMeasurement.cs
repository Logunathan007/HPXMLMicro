using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyScore.Domain.Entityies
{
    public class AirInfiltrationMeasurement
    {
        [Key]
        public Guid Id { get; set; }
        public double HousePressure { get; set; }
        public string UnitofMeasure { get; set; }
        public float AirLeakage { get; set; }
        public string LeakinessDescription { get; set; }
        public Guid AboutId { get; set; }
        public About About { get; set; }
    }
}
