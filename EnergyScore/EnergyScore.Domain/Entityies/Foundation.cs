using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyScore.Domain.Entityies
{
    public class Foundation
    {
        public Guid Id { get; set; }
        public string FoundationType { get; set;}
        public bool Finished { get; set;}
        public bool Conditioned { get; set;}
        public bool Walkout { get; set;}
        public virtual ICollection<FoundationWall>? FoundationWalls { get; set; }
    }
}
