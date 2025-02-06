
using EnergyScore.Application.Mappers.DTOS;
using EnergyScore.Application.Templates.HPXMLs;
using System.Xml;
using System.Xml.Serialization;

namespace EnergyScore.Application.Operations
{
    public interface IHPXMLOperations
    {
        public HPXML GetHPXMLObj(Guid buildingId, AddressDTO addressDTO, AboutDTO aboutDTO);
        public List<AirInfiltrationMeasurement> AirInfiltrationMeasurementConvertor(AboutDTO aboutDTO);
    }
    public class HPXMLOperations : IHPXMLOperations
    {
        public List<AirInfiltrationMeasurement>  AirInfiltrationMeasurementConvertor(AboutDTO aboutDTO)
        {
            List<AirInfiltrationMeasurement> airInFilMeasure = new List<AirInfiltrationMeasurement>();
            foreach (var item in aboutDTO.AirInfiltrationMeasurements)
            {
                airInFilMeasure.Add(new AirInfiltrationMeasurement
                {
                    SystemIndentifier = new SystemIndentifier
                    {
                        Id = item.Id.ToString(),
                    },
                    LeakinessDescription = item.LeakinessDescription,
                    BuildingAirLeakage = new BuildingAirLeakage()
                    {
                        UnitofMeasure = item.UnitofMeasure,
                        AirLeakage = item.AirLeakage
                    }
                });
            }
            return airInFilMeasure;
        }
        public HPXML GetHPXMLObj(Guid buildingId, AddressDTO addressDTO, AboutDTO aboutDTO)
        {
            var hpxml = new HPXML
            {
                XMLTransactionHeaderInformation = new XMLTransactionHeaderInformation
                {
                    XMLType = "",
                    XMLGeneratedBy = "Inspection Depot",
                    Transaction = "create"
                },
                SoftwareInfo = new SoftwareInfo(),
                Building = new Building
                {
                    BuildingID = new BuildingID
                    {
                        Id = buildingId.ToString()
                    },
                    Site = new Site
                    {
                        Address = new Address
                        {
                            Address1 = addressDTO.Address1,
                            Address2 = addressDTO.Address2,
                            CityMunicipality = addressDTO.City,
                            StateCode = addressDTO.State,
                            ZipCode = addressDTO.Zipcode
                        }
                    },
                    ProjectStatus = new ProjectStatus
                    {
                        EventType = "quality assurance/monitoring",
                        Date = DateTime.Today.Date
                    },
                    BuildingDetails = new BuildingDetails
                    {
                        BuildingSummary = new BuildingSummary
                        {
                            Site = new BSSite
                            {
                                OrientationOfFrontOfHome = aboutDTO.OrientationOfFrontOfHome,
                                AzimuthOfFrontOfHome = aboutDTO.AzimuthOfFrontOfHome
                            },
                            BuildingConstruction = new BuildingConstruction
                            {
                                YearBuilt = aboutDTO.YearBuilt,
                                ResidentialFacilityType = aboutDTO.ResidentialFacilityType,
                                NumberofConditionedFloorsAboveGrade = aboutDTO.NumberofConditionedFloorsAboveGrade,
                                AverageCeilingHeight = aboutDTO.AverageCeilingHeight,
                                NumberofBedrooms = aboutDTO.NumberofBedrooms,
                                ConditionedFloorArea = aboutDTO.ConditionedFloorArea,
                                ConditionedBuildingVolume = aboutDTO.ConditionedBuildingVolume,
                            }
                        },
                        Enclosure = new Enclosure
                        {
                            AirInfiltration = new AirInfiltration
                            {
                                AirInfiltrationMeasurement = this.AirInfiltrationMeasurementConvertor(aboutDTO)

                            }
                        }
                    }
                }
            };
            return hpxml;

        }
    }
}
