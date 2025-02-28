using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.AboutDTOS;
using EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS;
using EnergyScore.Application.Mappers.DTOS.AddressDTOS;
using EnergyScore.Application.Mappers.DTOS.CommonDTOS;
using EnergyScore.Domain.Entityies.AboutModels;
using EnergyScore.Domain.Entityies.ZoneFloorModels;
using EnergyScore.Domain.Entityies.CommonModels;
using EnergyScore.Domain.Entityies.AddressModels;
using EnergyScore.Domain.Entityies.ZoneRoofModels;
using EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS;
using EnergyScore.Domain.Entityies.ZoneWallModels;
using EnergyScore.Application.Mappers.DTOS.ZoneWallDTOS;
using EnergyScore.Application.Mappers.DTOS.DistributionSystemDTOS;
using EnergyScore.Domain.Entityies.DistributionSystemModels;
using EnergyScore.Domain.Entityies.HVACPlantModels;
using EnergyScore.Application.Mappers.DTOS.HVACPlantDTOS;
using EnergyScore.Domain.Entityies.WaterHeatingModels;
using EnergyScore.Application.Mappers.DTOS.WaterHeating;
using EnergyScore.Domain.Entityies.PhotovoltaicsModels;
using EnergyScore.Application.Mappers.DTOS.PhotovoltaicsDTOS;

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
            CreateMap<ZoneRoof, ZoneRoofDTO>().ReverseMap();
            CreateMap<Attic, AtticDTO>().ReverseMap();
            CreateMap<Roof, RoofDTO>().ReverseMap();
            CreateMap<Wall, WallDTO>().ReverseMap();
            CreateMap<AtticTypeDynamicOption,AtticTypeDynamicOptionDTO>().ReverseMap();
            CreateMap<Skylight, SkylightDTO>().ReverseMap();
            CreateMap<FrameTypeDynamicOptions, FrameTypeDynamicOptionsDTO>().ReverseMap();
            CreateMap<WallTypeDynamicOptions, WallTypeDynamicOptionsDTO>().ReverseMap();    
            CreateMap<Window, WindowDTO>().ReverseMap();
            CreateMap<ZoneWall, ZoneWallDTO>().ReverseMap();
            CreateMap<InsulationMaterialDynamicOptionsDTO, InsulationMaterialDynamicOptions>().ReverseMap();
            CreateMap<DistributionSystem, DistributionSystemDTO>().ReverseMap();
            CreateMap<Duct, DuctDTO>().ReverseMap();
            CreateMap<DistributionSystems, DistributionSystemsDTO>().ReverseMap();
            CreateMap<HeatingSystem, HeatingSystemDTO>().ReverseMap();
            CreateMap<CoolingSystem, CoolingSystemDTO>().ReverseMap();
            CreateMap<HeatPump, HeatPumpDTO>().ReverseMap();
            CreateMap<HVACPlant, HVACPlantDTO>().ReverseMap();
            CreateMap<WaterHeating, WaterHeatingDTO>().ReverseMap();
            CreateMap<WaterHeatingSystem, WaterHeatingSystemDTO>().ReverseMap();
            CreateMap<Photovoltaics, PhotovoltaicsDTO>().ReverseMap();
            CreateMap<PVSystem, PVSystemDTO>().ReverseMap();
        }
    }
}
