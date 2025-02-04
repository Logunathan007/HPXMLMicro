using EnergyScore.Domain.Entityies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyScore.Application.Mappers.DTOS
{
    public class AboutDTO
    {
        public string residentialFacilityType { get; set; }
        public int yearBuilt { get; set; }
        public int numberofBedrooms { get; set; }
        public int numberofConditionedFloorsAboveGrade { get; set; }
        public int averageCeilingHeight { get; set; }
        public int conditionedBuildingVolume { get; set; }
        public int conditionedFloorArea { get; set; }
        public int azimuthOfFrontOfHome { get; set; }
        public string orientationOfFrontOfHome { get; set; }
        public ICollection<AirInfiltrationMeasurement> AirInfiltrationMeasurements { get; set; }
    }
}
