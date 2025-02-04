using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyScore.Domain.Entityies
{
    public class About
    {
        public Guid AboutId { get; set; }
        public string ResidentialFacilityType { get; set; }
        public int YearBuilt { get; set; }
        public int NumberofBedrooms  { get; set; }
        public int NumberofConditionedFloorsAboveGrade { get; set; }
        public int AverageCeilingHeight { get; set; }
        public int ConditionedBuildingVolume { get; set; }
        public int ConditionedFloorArea { get; set; }
        public int AzimuthOfFrontOfHome { get; set; }
        public string OrientationOfFrontOfHome { get; set; }
        public ICollection<AirInfiltrationMeasurement> AirInfiltrationMeasurements { get; set; }
    }
}
