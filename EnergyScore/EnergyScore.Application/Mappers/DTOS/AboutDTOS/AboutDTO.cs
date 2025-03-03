namespace EnergyScore.Application.Mappers.DTOS.AboutDTOS
{
    public class AboutDTO
    {
        public Guid Id { get; set; }
        public string ResidentialFacilityType { get; set; }
        public int YearBuilt { get; set; }
        public int NumberofBedrooms { get; set; }
        public double NumberofConditionedFloorsAboveGrade { get; set; }
        public double? AverageCeilingHeight { get; set; }
        public double? ConditionedBuildingVolume { get; set; }
        public string? ManufacturedHomeSections { get; set; }
        public double? ConditionedFloorArea { get; set; }
        public int? AzimuthOfFrontOfHome { get; set; }
        public string? OrientationOfFrontOfHome { get; set; }
        public string? Comments { get; set; }
        public ICollection<AirInfiltrationMeasurementDTO> AirInfiltrationMeasurements { get; set; }
    }
}
