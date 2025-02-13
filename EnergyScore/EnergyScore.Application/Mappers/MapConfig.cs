using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.AboutDTOS;
using EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS;
using EnergyScore.Application.Mappers.DTOS.AddressDTOS;
using EnergyScore.Application.Mappers.DTOS.CommonDTOS;
using EnergyScore.Domain.Entityies.AboutModels;
using EnergyScore.Domain.Entityies.ZoneFloorModels;
using EnergyScore.Domain.Entityies.CommonModels;
using EnergyScore.Domain.Entityies.AddressModels;

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
            CreateMap<Foundation, FoundationDTO>().ReverseMap();
            CreateMap<FoundationTypeDynamicOption, FoundationTypeDynamicOptionDTO>().ReverseMap();
            CreateMap<FoundationWall, FoundationWallDTO>().ReverseMap();
            CreateMap<FrameFloor, FrameFloorDTO>().ReverseMap();
            CreateMap<Insulation, InsulationDTO>().ReverseMap();
            CreateMap<PerimeterInsulation, PerimeterInsulationDTO>().ReverseMap();
            CreateMap<Slab, SlabDTO>().ReverseMap();
            CreateMap<ZoneFloor, ZoneFloorDTO>().ReverseMap();
        }
    }
}
