﻿

using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.AddressDTOS;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Domain.Entityies.AddressModels;
using EnergyScore.Persistence.DBConnection;

namespace EnergyScore.Application.Operations
{
    public interface IBuildingOperations
    {
        public ResponseForBuilding CreateBuilding(BuildingDTO building);
        public Response UpdateAboutId(Guid BuildingId, Guid AboutId);
        public BuildingDTO GetBuildingById(Guid BuildingId);
        public Response UpdateZoneFloorId(Guid BuildingId, Guid ZoneFloorId);
        public Response UpdateZoneRoofId(Guid BuildingId, Guid ZoneRoofId);
    }
    public class BuildingOperations : IBuildingOperations
    {
        private readonly DbConnect _dbConnect;
        private readonly IMapper _mapper;
        public BuildingOperations(DbConnect dbConnect, IMapper mapConfig)
        {
            _dbConnect = dbConnect;
            _mapper = mapConfig;
        }

        public ResponseForBuilding CreateBuilding(BuildingDTO buildingDTO)
        {
            Building building = _mapper.Map<Building>(buildingDTO);
            this._dbConnect.Buildings.Add(building);
            this._dbConnect.SaveChanges();
            return new ResponseForBuilding() { Failed = false, Message = "Building Added Successfully", BuildingId = building.Id };
        }

        public Response UpdateAboutId(Guid BuildingId,Guid AboutId)
        {
            Building building = this._dbConnect.Buildings.FirstOrDefault(obj => obj.Id == BuildingId);
            if (building == null) {
                return new Response() { Failed = true, Message = "Building Not Found" };
            }
            building.AboutId = AboutId;
            this._dbConnect.SaveChanges();
            return new Response() { Failed = false, Message = "AboutId is Updated Successfully" };
        }
        public Response UpdateZoneFloorId(Guid BuildingId, Guid ZoneFloorId)
        {
            Building building = this._dbConnect.Buildings.FirstOrDefault(obj => obj.Id == BuildingId);
            if (building == null) {
                return new Response() { Failed = true, Message = "Building Not Found" };
            }
            building.ZoneFloorId = ZoneFloorId;
            this._dbConnect.SaveChanges();
            return new Response() { Failed = false, Message = "ZoneFloorId is Updated Successfully" };
        }
        public Response UpdateZoneRoofId(Guid BuildingId, Guid ZoneRoofId)
        {
            Building building = this._dbConnect.Buildings.FirstOrDefault(obj => obj.Id == BuildingId);
            if (building == null)
            {
                return new Response() { Failed = true, Message = "Building Not Found" };
            }
            building.ZoneRoofId = ZoneRoofId;
            this._dbConnect.SaveChanges();
            return new Response() { Failed = false, Message = "ZoneRoofId is Updated Successfully" };
        }
        public BuildingDTO GetBuildingById(Guid BuildingId)
        {
            Building building = this._dbConnect.Buildings.FirstOrDefault(obj => obj.Id == BuildingId);
            if (building == null) {
                return null;
            }
            BuildingDTO buildingDTO = _mapper.Map<BuildingDTO>(building);
            return buildingDTO;
        }
    }
}
