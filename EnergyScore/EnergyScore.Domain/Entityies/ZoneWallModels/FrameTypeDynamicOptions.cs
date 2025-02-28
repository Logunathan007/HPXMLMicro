namespace EnergyScore.Domain.Entityies.ZoneWallModels
{
    public class FrameTypeDynamicOptions
    {
        public Guid Id { get; set; }   
        public bool? ThermalBreak {  get; set; }
        public Guid WindowId {  get; set; }
        public Window Window { get; set; }
    }
}
