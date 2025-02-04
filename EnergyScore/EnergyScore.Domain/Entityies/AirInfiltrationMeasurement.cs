using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyScore.Domain.Entityies
{
    public class AirInfiltrationMeasurement
    {
        public Guid AirInfiltrationMeasurementId { get; set; }
        public int HousePressure { get; set; }
        public string UnitofMeasure { get; set; }
        public int AirLeakage { get; set; }
        public string LeakinessDescription { get; set; }
        public Guid AboutId { get; set; }
        public About About { get; set; }
    }
}
