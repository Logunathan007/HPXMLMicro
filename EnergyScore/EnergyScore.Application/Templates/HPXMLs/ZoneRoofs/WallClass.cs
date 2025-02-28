﻿
using System.Xml.Serialization;

namespace EnergyScore.Application.Templates.HPXMLs.ZoneRoofs
{
    public class Walls
    {
        [XmlElement]
        public List<Wall> Wall { get; set; }
    }
    public class Wall
    {
        public SystemIdentifier SystemIdentifier { get; set; }
        public string ExteriorAdjacentTo {  get; set; }
        public string InteriorAdjacentTo {  get; set; }
        public string AtticWallType { get; set; }
        public WallType WallType { get; set; }
        public double Area { get; set; }
        public string Orientation { get; set; }
        public int Azimuth { get; set; }
        public string Siding { get; set; }
        [XmlElement("Insulation")]
        public List<Insulation> Insulations { get; set; }
    }
    public class WallType
    {
        public WoodStud? WoodStud { get; set; }
        public DoubleWoodStud? DoubleWoodStud { get; set; }
        public ConcreteMasonryUnit? ConcreteMasonryUnit { get; set; }
        public StructurallyInsulatedPanel? StructurallyInsulatedPanel {  get; set; }
        public InsulatedConcreteForms? InsulatedConcreteForms {get;set;}
        public SteelFrame? SteelFrame {get;set;}
        public SolidConcrete? SolidConcrete {get;set;}
        public StructuralBrick? StructuralBrick {get;set;}
        public StrawBale? StrawBale {get;set;}
        public Stone? Stone {get;set;}
        public LogWall? LogWall {get;set;}
        public Adobe? Adobe {get;set;}
        public Other? Other { get; set; }
    }
    public class WoodStud
    {
        public string? ExpandedPolystyreneSheathing {get;set;}
        public bool? OptimumValueEngineering {get;set;}
        public string? FramingType { get; set; }
    }
    public class DoubleWoodStud
    {
        public bool? Staggered { get; set; }
    }
    public class ConcreteMasonryUnit{}
    public class StructurallyInsulatedPanel{}
    public class InsulatedConcreteForms {}
    public class SteelFrame {}
    public class SolidConcrete {}
    public class StructuralBrick {}
    public class StrawBale {}
    public class Stone {}
    public class LogWall {}
    public class Adobe {}
}
