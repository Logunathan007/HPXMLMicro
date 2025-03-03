using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS;
using EnergyScore.Application.Mappers.DTOS.ZoneWallDTOS;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Domain.Entityies.ZoneWallModels;
using EnergyScore.Persistence.DBConnection;
using Microsoft.EntityFrameworkCore;


namespace EnergyScore.Application.Operations
{
    public interface IZoneWallOperations
    {
        public ResponseForZoneWall AddZoneWall(ZoneWallDTO zoneWallDTO, Guid buildingId);
        public IEnumerable<WallDTO> GetWallsByBuildingId(Guid buildingId);
        public IEnumerable<WindowDTO> GetWindowByBuildingId(Guid buildingId);
    }
    public class ZoneWallOperations : IZoneWallOperations
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
        public IEnumerable<WallDTO> GetWallsByBuildingId(Guid buildingId)
        {
            if (buildingId == null || buildingId == Guid.Empty) { return null; }
            var walls = _dbConnect.Walls
                .Include(obj => obj.Insulation).ThenInclude(obj => obj.Layers).ThenInclude(obj => obj.InsulationMaterialDynamicOptions)
                .Include(obj => obj.WallTypeDynamicOptions)
                .Where(x => x.BuildingId == buildingId).ToList();
            return _mapper.Map<IEnumerable<WallDTO>>(walls);
        }
        public IEnumerable<WindowDTO> GetWindowByBuildingId(Guid buildingId)
        {
            if (buildingId == null || buildingId == Guid.Empty) { return null; }
            IEnumerable<Window> windows = _dbConnect.Windows.Include(obj => obj.FrameTypeDynamicOptions).Where(obj=> obj.BuildingId == buildingId).ToList();
            return _mapper.Map<IEnumerable<WindowDTO>>(windows);
        }
    }
}
