using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
