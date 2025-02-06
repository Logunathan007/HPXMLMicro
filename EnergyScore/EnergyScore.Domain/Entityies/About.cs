
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies
{
    public class About
    {
        [Key]
        public Guid Id { get; set; }
        public string ResidentialFacilityType { get; set; }
        public int YearBuilt { get; set; }
        public int NumberofBedrooms  { get; set; }
        public float NumberofConditionedFloorsAboveGrade { get; set; }
        public float AverageCeilingHeight { get; set; }
        public float ConditionedBuildingVolume { get; set; }
        public float ConditionedFloorArea { get; set; }
        public int AzimuthOfFrontOfHome { get; set; }
        public string OrientationOfFrontOfHome { get; set; }
        public ICollection<AirInfiltrationMeasurement> AirInfiltrationMeasurements { get; set; }
    }
}
