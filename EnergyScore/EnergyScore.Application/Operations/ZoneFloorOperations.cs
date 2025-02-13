using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.AboutDTOS;
using EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Domain.Entityies.AboutModels;
using EnergyScore.Domain.Entityies.ZoneFloorModels;
using EnergyScore.Persistence.DBConnection;
using Microsoft.EntityFrameworkCore;
using System.Drawing;


namespace EnergyScore.Application.Operations
{
    public interface IZoneFloorOperatoins
    {
        public ResponseForZoneFloor AddZoneFloor(ZoneFloorDTO floorDTO, Guid BuildingId);
        public ZoneFloorDTO GetZoneFloorById(Guid? zoneFloorId);
    }
    public class ZoneFloorOperations : IZoneFloorOperatoins
    {
        private readonly DbConnect _dbConnect;
        private readonly IMapper _mapper;
        private readonly IBuildingOperations _buildingOperations;
        public ZoneFloorOperations(DbConnect dbConnect, IMapper mapConfig, IBuildingOperations buildingOperations) {
            _dbConnect = dbConnect;
            _mapper = mapConfig;
            _buildingOperations = buildingOperations;
        }
        public ResponseForZoneFloor AddZoneFloor(ZoneFloorDTO floorDTO,Guid BuildingId)
        {
            ZoneFloor floor = _mapper.Map<ZoneFloor>(floorDTO);
            this._dbConnect.ZoneFloors.Add(floor);
            this._dbConnect.SaveChanges();
            this._buildingOperations.UpdateZoneFloorId(BuildingId, floor.Id);
            return new ResponseForZoneFloor { Failed = false, Message = "ZoneFloor Added Successfully", ZoneFloorId = floor.Id };
        }
        public ZoneFloorDTO GetZoneFloorById(Guid? zoneFloorId)
        {
            if(zoneFloorId == null){
                return null;
            }
            ZoneFloor floor = _dbConnect.ZoneFloors
                .Include(obj => obj.Foundations)
                    .ThenInclude(obj => obj.FoundationTypeDynamicOptions)
                .Include(obj => obj.Foundations)
                    .ThenInclude(obj => obj.FoundationWalls)
                    .ThenInclude(obj => obj.Insulations)
                .Include(obj => obj.Foundations)
                    .ThenInclude(obj => obj.FrameFloors)
                    .ThenInclude(obj => obj.Insulations)
                .Include(obj => obj.Foundations)
                    .ThenInclude(obj => obj.Slabs)
                    .ThenInclude(obj => obj.PerimeterInsulations)
                .FirstOrDefault(obj => obj.Id == zoneFloorId);
            if (floor == null)
            {
                return null;
            }
            ZoneFloorDTO zoneFloor = _mapper.Map<ZoneFloorDTO>(floor);
            return zoneFloor;
        }
    }
}
