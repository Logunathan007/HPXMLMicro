using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS;
using EnergyScore.Domain.Entityies.ZoneFloorModels;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Persistence.DBConnection;
using Microsoft.EntityFrameworkCore;


namespace EnergyScore.Application.Operations
{
    public interface IZoneFloorOperatoins
    {
        public ResponseForZoneFloor AddZoneFloor(ZoneFloorDTO floorDTO, Guid BuildingId);
        public ZoneFloorDTO GetZoneFloorById(Guid? zoneFloorId);
        public IEnumerable<FoundationDTO> GetFoundationsByBuildingId(Guid BuildingId);
        public IEnumerable<FoundationWallDTO> GetFoundationWallsByBuildingId(Guid BuildingId);
        public IEnumerable<FrameFloorDTO> GetFrameFloorByBuildingId(Guid BuildingId);
        public IEnumerable<SlabDTO> GetSlabByBuildingId(Guid BuildingId);
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
            this._buildingOperations.UpdateZoneFloorId(BuildingId, floor.Id);
            this._dbConnect.SaveChanges();
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
                    .ThenInclude(obj => obj.Insulation)
                .Include(obj => obj.Foundations)
                    .ThenInclude(obj => obj.FrameFloors)
                    .ThenInclude(obj => obj.Insulation)
                .Include(obj => obj.Foundations)
                    .ThenInclude(obj => obj.Slabs)
                    .ThenInclude(obj => obj.PerimeterInsulation)
                .Include(obj => obj.Foundations)
                .FirstOrDefault(obj => obj.Id == zoneFloorId);

            if (floor == null)
            {
                return null;
            }
            ZoneFloorDTO zoneFloor = _mapper.Map<ZoneFloorDTO>(floor);
            return zoneFloor;
        }
        public IEnumerable<FoundationDTO> GetFoundationsByBuildingId(Guid BuildingId)
        {
            if(BuildingId == Guid.Empty || BuildingId == null) { return null; }
            IEnumerable<Foundation> found = _dbConnect.Foundations
                .Include(obj=>obj.FoundationTypeDynamicOptions)
                .Include(obj=>obj.FoundationWalls)
                .Include(obj=>obj.Slabs)
                .Include(obj=>obj.FrameFloors)
                .Where(obj => obj.BuildingId == BuildingId).ToList();

            return _mapper.Map<IEnumerable<FoundationDTO>>(found);
        }
        public IEnumerable<FoundationWallDTO> GetFoundationWallsByBuildingId(Guid BuildingId) {
            if(BuildingId == Guid.Empty || BuildingId == null) { return null; }
            IEnumerable<FoundationWall> foundWall = _dbConnect.FoundationWalls
                .Include(obj => obj.Insulation).ThenInclude(obj => obj.Layers).ThenInclude(obj => obj.InsulationMaterialDynamicOptions)
                .Where(obj => obj.BuildingId == BuildingId).ToList();
            return _mapper.Map<IEnumerable<FoundationWallDTO>>(foundWall);
        }
        public IEnumerable<FrameFloorDTO> GetFrameFloorByBuildingId(Guid BuildingId)
        {
            if (BuildingId == Guid.Empty || BuildingId == null) { return null; }
            IEnumerable<FrameFloor> frameFloor = _dbConnect.FrameFloors
                .Include(obj => obj.Insulation).ThenInclude(obj=>obj.Layers).ThenInclude(obj=>obj.InsulationMaterialDynamicOptions)
                .Where(obj => obj.BuildingId == BuildingId).ToList();
            return _mapper.Map<IEnumerable<FrameFloorDTO>>(frameFloor);
        }
        public IEnumerable<SlabDTO> GetSlabByBuildingId(Guid BuildingId)
        {
            if (BuildingId == Guid.Empty || BuildingId == null) { return null; }
            IEnumerable<Slab> slabs = _dbConnect.Slabs
                .Include(obj => obj.PerimeterInsulation).ThenInclude(obj => obj.Layers).ThenInclude(obj => obj.InsulationMaterialDynamicOptions)
                .Where(obj => obj.BuildingId == BuildingId).ToList();
            return _mapper.Map<IEnumerable<SlabDTO>>(slabs);
        }
    }
}
