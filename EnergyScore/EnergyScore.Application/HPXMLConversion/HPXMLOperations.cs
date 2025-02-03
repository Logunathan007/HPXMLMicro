using EnergyScore.Application.Mappers.DTOS;
using EnergyScore.Application.Operations;
using EnergyScore.Domain.HPXMLClasses;
using System.Xml.Serialization;

namespace EnergyScore.Application.HPXMLConversion
{
    public interface IHPXMLOperations
    {
        void Convertor(AddressDTO address);
    }
    public class HPXMLOperations : IHPXMLOperations
    {
        public void Convertor(AddressDTO address)
        {
            var hpxml = new HPXML
            {
                XMLTransactionHeaderInformation = new XMLTransactionHeaderInformation
                {
                    XMLType = "",
                    Transaction = "create"
                },
                Building = new Building
                {
                    Site = new Site
                    {
                        Address = new Address
                        {
                            Address1 = address.Address1,
                            Address2 = address.Address2,
                            CityMunicipality = address.City,
                            StateCode = address.State,
                            ZipCode = address.Zipcode
                        }
                    },
                    ProjectStatus = new ProjectStatus
                    {
                        EventType = "quality assurance/monitoring",
                        Date = DateTime.Parse("2025-01-23")
                    },
                    BuildingDetails = new BuildingDetails
                    {
                        BuildingSummary = new BuildingSummary
                        {
                            BuildingConstruction = new BuildingConstruction
                            {
                                ResidentialFacilityType = "multi-family - condos"
                            }
                        }
                    }
                }
            };

            // Determine the project's directory dynamically
            var projectDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../");
            var outputPath = Path.Combine(projectDirectory, "output.xml");

            // Serialize the HPXML object to the file
            var serializer = new XmlSerializer(typeof(HPXML));
            using (var writer = new StreamWriter(outputPath))
            {
                serializer.Serialize(writer, hpxml);
            }

            Console.WriteLine($"Serialization complete. File saved at: {Path.GetFullPath(outputPath)}");
        }
    }
}
