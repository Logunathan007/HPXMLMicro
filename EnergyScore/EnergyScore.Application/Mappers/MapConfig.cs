using AutoMapper;
using EnergyScore.Domain.Entityies;
using EnergyScore.Application.Mappers.DTOS;

namespace EnergyScore.Application.Mappers
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<AirInfiltrationMeasurement, AirInfiltrationMeasurementDTO>().ReverseMap();
            CreateMap<About, AboutDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Building, BuildingDTO>().ReverseMap();
        }
    }
}
