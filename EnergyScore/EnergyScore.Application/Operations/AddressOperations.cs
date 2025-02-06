using EnergyScore.Persistence.DBConnection;
using EnergyScore.Domain.Entityies;
using AutoMapper;
using EnergyScore.Application.Mappers.DTOS;
using EnergyScore.Application.Templates.Responses;

namespace EnergyScore.Application.Operations
{
    public interface IAddressOperations
    {
        ResponseForAddress AddAddress(AddressDTO address);
        public AddressDTO GetAddressById(Guid AddressId);
    }
    public class AddressOperations : IAddressOperations
    {
        private readonly DbConnect _dbConnect;
        private readonly IMapper _mapper;
        private readonly BuildingOperations _buildingOperations;
        public AddressOperations(DbConnect dbConnect,IMapper mapConfig,IBuildingOperations buildingOperations)
        {
            _dbConnect = dbConnect; 
            _mapper = mapConfig;
            _buildingOperations = (BuildingOperations)buildingOperations;
        }
        public ResponseForAddress AddAddress(AddressDTO addressDTO)
        {
            Address address = _mapper.Map<Address>(addressDTO);
            _dbConnect.Addresss.Add(address);
            _dbConnect.SaveChanges();
            BuildingDTO buildingDTO = new BuildingDTO() { AddressId = address.Id };
            ResponseForBuilding building = _buildingOperations.CreateBuilding(buildingDTO);
            return new ResponseForAddress() { Failed = false, Message = "Address Added Successfully", AddressId = address.Id , BuildingId = building.BuildingId};
        }

        public AddressDTO GetAddressById(Guid AddressId)
        {
            Address address = _dbConnect.Addresss.FirstOrDefault(obj => obj.Id == AddressId);
            if (address == null)
            {
                return null;
            }
            AddressDTO addressDTO = _mapper.Map<AddressDTO>(address);
            return addressDTO;
        }
    }
}
