
using System.Xml.Serialization;

namespace EnergyScore.Application.Templates.HPXMLs.Systems
{
    public class HAVC
    {
        public HVACDistribution HVACDistribution { get; set; }
    }
    public class HVACDistribution
    {
        public SystemIdentifier SystemIdentifier { get; set; }
        public DistributionSystemType DistributionSystemType { get; set; }
        public HVACDistributionImprovement HVACDistributionImprovement { get; set; }
    }
    public class HVACDistributionImprovement
    {
        public bool DuctSystemSealed { get; set; }
    }
    public class DistributionSystemType
    {
        public AirDistribution AirDistribution { get; set; }
    }
    public class AirDistribution
    {
        public DuctLeakageMeasurement DuctLeakageMeasurement { get; set; }
        public Ducts Ducts { get; set; }
    }
    public class Ducts
    {
        public SystemIdentifier SystemIdentifier { get; set; }
        [XmlElement("DuctInsulationMaterial")]
        public InsulationMaterial DuctInsulationMaterial { get; set; }
        public double DuctInsulationRValue { get; set; }
        public double DuctInsulationThickness { get; set; }
        public string DuctLocation { get; set; }
        public double FractionDuctArea { get; set; }
    }
    public class DuctLeakageMeasurement
    {
        public string DuctType { get; set; }
        public string LeakinessObservedVisualInspection { get; set; }
        public DuctLeakage DuctLeakage { get; set; }
    }
    public class DuctLeakage
    {
        public string Units { get; set; }
        public double Value { get; set; }
        public string TotalOrToOutside { get; set; }
    }
    public class HpxmlSystems
    {
        public HAVC HAVC { get; set; }
    }
}
