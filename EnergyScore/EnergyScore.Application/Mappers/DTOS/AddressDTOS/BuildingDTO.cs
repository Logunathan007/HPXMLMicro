namespace EnergyScore.Application.Mappers.DTOS.AddressDTOS
{
    public class BuildingDTO
    {
        public Guid? AboutId { get; set; } 
        public Guid AddressId { get; set; }
        public Guid? ZoneFloorId { get; set; }  
        public Guid? ZoneRoofId { get; set; } 
        public Guid? ZoneWallId { get; set; } 
        public Guid? HAVCPlantId { get; set; }
        public Guid? DistributionSystemsId { get; set; }
    }
}
