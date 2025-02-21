using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.ZoneWallDTOS;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Domain.Entityies.ZoneWallModels;
using EnergyScore.Persistence.DBConnection;


namespace EnergyScore.Application.Operations
{
    public interface IZoneWallOperations
    {
        public ResponseForZoneWall AddZoneWall(ZoneWallDTO zoneWallDTO, Guid buildingId);

    }
    public class ZoneWallOperations:IZoneWallOperations
    {
        private readonly DbConnect _dbConnect;
        private readonly IBuildingOperations _buildingOperations;
        private readonly IMapper _mapper;
        public ZoneWallOperations(DbConnect dbConnect, IMapper mapper, IBuildingOperations buildingOperations)
        {
            _dbConnect = dbConnect;
            _mapper = mapper;
            _buildingOperations = buildingOperations;
        }
        public ResponseForZoneWall AddZoneWall(ZoneWallDTO zoneWallDTO, Guid buildingId)
        {
            ZoneWall zoneWall = _mapper.Map<ZoneWall>(zoneWallDTO);
            _dbConnect.ZoneWalls.Add(zoneWall);
            _dbConnect.SaveChanges();
            _buildingOperations.UpdateZoneWallId(buildingId, zoneWall.Id);
            return new ResponseForZoneWall { Failed = false, Message = "ZoneWall Added Successfully", ZoneWallId = zoneWall.Id };
        }
    }
}
