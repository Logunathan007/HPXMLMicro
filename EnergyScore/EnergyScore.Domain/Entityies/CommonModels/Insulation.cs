﻿
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies.CommonModels
{
    public class Insulation
    {
        [Key]
        public Guid Id { get; set; }
        public double NominalRValue { get; set; }
        public double AssemblyEffectiveRValue { get; set; }
    }
}
