using EnergyScore.Persistence.DBConnection;
using EnergyScore.Domain.Entityies;
using System.Collections;
using EnergyScore.Application.Mappers;
using AutoMapper;
using EnergyScore.Application.Mappers.DTOS;

namespace EnergyScore.Application.Operations
{
    public interface IAddressOperations
    {
       void AddAddress(AddressDTO address);
       AddressDTO GetAddress(Guid id);
    }
    public class AddressOperations : IAddressOperations
    {
        private readonly DbConnect _dbConnect;
        private readonly IMapper _mapper;
        public AddressOperations(DbConnect dbConnect,IMapper mapConfig)
        {
            _dbConnect = dbConnect; 
            _mapper = mapConfig;
        }
        public void AddAddress(AddressDTO address)
        {
            Address add = _mapper.Map<Address>(address);
            _dbConnect.Address.Add(add);
            _dbConnect.SaveChanges();
        }
        public AddressDTO GetAddress(Guid id)
        {
            return _mapper.Map<AddressDTO>(_dbConnect.Address.Find(id));
        }
    }
}
