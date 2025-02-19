namespace EnergyScore.Application.Mappers.DTOS.AddressDTOS
{
    public class BuildingDTO
    {
        public Guid? AboutId { get; set; } = null;
        public Guid AddressId { get; set; }
        public Guid? ZoneFloorId { get; set; } = null;
        public Guid? ZoneRoofId { get; set; } = null;
    }
}
