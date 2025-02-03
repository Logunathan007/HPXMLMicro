using AutoMapper;
using EnergyScore.Domain.Entityies;
using EnergyScore.Application.Mappers.DTOS;

namespace EnergyScore.Application.Mappers
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
