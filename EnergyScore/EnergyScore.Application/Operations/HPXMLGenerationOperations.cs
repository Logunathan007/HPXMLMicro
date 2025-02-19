
using AutoMapper;
using EnergyScore.Application.Templates.HPXMLs;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Persistence.DBConnection;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using EnergyScore.Application.Mappers.DTOS.AddressDTOS;
using EnergyScore.Application.Mappers.DTOS.AboutDTOS;

namespace EnergyScore.Application.Operations
{
    public interface IHPXMLGenerationOperations
    {
        public HPXMLGenerationResponse GenerateHpxml(Guid BuildingId);
        public string GenerateHPXMLString(HPXML hpxml);
        public string GenerateHPXMLStringBase64Encode(HPXML hpxml);
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    public class HPXMLGenerationOperations : IHPXMLGenerationOperations
    {
        private readonly DbConnect _dbConnect;
        private readonly IMapper _mapper;
        private readonly IBuildingOperations _buildingOperations;
        private readonly IAddressOperations _addressOperations;
        private readonly IAboutOperations _aboutOperations;
        private readonly IHPXMLOperations _hpxmlOperations;
        private readonly IZoneFloorOperatoins _zoneFloorOperatoins;
        public HPXMLGenerationOperations(
            DbConnect dbConnect,
            IMapper mapper,
            IBuildingOperations buildingOperations, 
            IAddressOperations addressOperations,
            IAboutOperations aboutOperations,
            IHPXMLOperations hpxmlOperations,
            IZoneFloorOperatoins zoneFloorOperatoins)
        {
            _dbConnect = dbConnect;
            _mapper = mapper;
            _buildingOperations = buildingOperations;
            _addressOperations = addressOperations;
            _aboutOperations = aboutOperations;
            _hpxmlOperations = hpxmlOperations;
            _zoneFloorOperatoins = zoneFloorOperatoins;
        }
        public HPXMLGenerationResponse GenerateHpxml(Guid BuildingId)
        {
            // Check if building exists
            BuildingDTO buildingDTO = _buildingOperations.GetBuildingById(BuildingId);
            if (buildingDTO == null)
            {
                return new HPXMLGenerationResponse { Failed = true, Message = "Building not Found" };
            }

            // Check if address exists
            AddressDTO addressDTO = _addressOperations.GetAddressById(buildingDTO.AddressId);
            if (addressDTO == null)
            {
                return new HPXMLGenerationResponse { Failed = true, Message = "Address not Found for your Building" };
            }

            // Check if about exists
            if (buildingDTO?.AboutId == null)
            {
                return new HPXMLGenerationResponse { Failed = true, Message = "About not Found for your Building" };
            }
            AboutDTO aboutDTO = _aboutOperations.GetAboutById(buildingDTO.AboutId);

            HPXML hpxml = _hpxmlOperations.GetHPXMLObj(BuildingId, addressDTO, aboutDTO);

            return new HPXMLGenerationResponse { Failed = false, Message = "About not Found for your Building" , hPXML = hpxml }; ;
        }
        public string GenerateHPXMLString(HPXML hpxml)
        {
            var serializer = new XmlSerializer(typeof(HPXML));
            string xmlString;
            using (var stringWriter = new Utf8StringWriter())
            {
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineOnAttributes = false,
                    OmitXmlDeclaration = false,
                    Encoding = Encoding.UTF8
                };
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    serializer.Serialize(xmlWriter, hpxml);
                }
                xmlString = stringWriter.ToString();
            }
            return xmlString;
        }

        public string GenerateHPXMLStringBase64Encode(HPXML hpxml)
        {
            string base64HPXML = Convert.ToBase64String(Encoding.UTF8.GetBytes(GenerateHPXMLString(hpxml)));
            return base64HPXML;
        }
    }
}
