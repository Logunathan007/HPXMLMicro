
namespace EnergyScore.Domain.Entityies.HVACPlantModels

{
    public class HVACPlant
    {
        public Guid Id { get; set; }
        public ICollection<HeatingSystem>? HeatingSystems { get; set; }  
        public ICollection<CoolingSystem>? CoolingSystems { get; set; }
        public ICollection<HeatPump>? HeatPumps { get; set; }
    }
}
