using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyScore.Domain.Entityies
{
    public class FoundationWall
    {
        public Guid Id { get; set; }
        public double area { get; set; }
        public double nominalRValue { get; set; }
        public double assemblyEffectiveRValue { get; set; }
    }
}
