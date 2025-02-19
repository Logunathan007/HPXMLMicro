
using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Domain.Entityies.ZoneRoofModels;
using EnergyScore.Persistence.DBConnection;
using Microsoft.EntityFrameworkCore;

namespace EnergyScore.Application.Operations
{
    public interface IZoneRoofOperations
    {
        public ResponseForZoneRoof AddZoneRoof(ZoneRoofDTO zoneRoofDTO, Guid buildingId);
        public ZoneRoofDTO GetZoneRoofById(Guid? zoneRoofId);
        public IEnumerable<RoofDTO> GetRoofsByBuildingId(Guid? buildingId);
        public IEnumerable<WallDTO> GetWallsByBuildingId(Guid? buildingId);
        public IEnumerable<AtticDTO> GetAtticsByBuildingId(Guid? buildingId);
    }
    public class ZoneRoofOperations : IZoneRoofOperations
    {
        private readonly DbConnect _dbConnect;
        private readonly IBuildingOperations _buildingOperations;
        private readonly IMapper _mapper;
        public ZoneRoofOperations(DbConnect dbConnect, IMapper mapper, IBuildingOperations buildingOperations)
        {
            _dbConnect = dbConnect;
            _mapper = mapper;
            _buildingOperations = buildingOperations;
        }
        public ResponseForZoneRoof AddZoneRoof(ZoneRoofDTO zoneRoofDTO,Guid buildingId)
        {
            ZoneRoof zoneRoof = _mapper.Map<ZoneRoof>(zoneRoofDTO);
            _dbConnect.ZoneRoofs.Add(zoneRoof);
            _dbConnect.SaveChanges();
            _buildingOperations.UpdateZoneRoofId(buildingId, zoneRoof.Id);
            return new ResponseForZoneRoof { Failed = false, Message = "ZoneRoof Added Successfully", ZoneRoofId = zoneRoof.Id };
        }
        public ZoneRoofDTO GetZoneRoofById(Guid? zoneRoofId)
        {
            if (zoneRoofId == null)
            {
                return null;
            }
            ZoneRoof roof = _dbConnect.ZoneRoofs
                .Include(obj => obj.Attics)
                    .ThenInclude(obj => obj.AtticTypeDynamicOptions)
                .Include(obj => obj.Attics)
                    .ThenInclude(obj => obj.FrameFloors)
                    .ThenInclude(obj => obj.Insulations)
                .Include(obj => obj.Attics)
                    .ThenInclude(obj => obj.Walls)
                .Include(obj => obj.Attics)
                    .ThenInclude(obj => obj.Roofs)
                    .ThenInclude(obj => obj.Insulations)
                .FirstOrDefault(obj => obj.Id == zoneRoofId);
            if (roof == null) { return null; }
            ZoneRoofDTO zoneRoofDTO = _mapper.Map<ZoneRoofDTO>(roof);
            return zoneRoofDTO;
        }
        public IEnumerable<AtticDTO> GetAtticsByBuildingId(Guid? buildingId)
        {
            if(buildingId == null || buildingId == Guid.Empty) { return null; }
            IEnumerable<Attic> attics = _dbConnect.Attics
                .Include(obj => obj.AtticTypeDynamicOptions)
                .Include(obj => obj.Roofs)
                .Include(obj => obj.Walls)
                .Include(obj => obj.FrameFloors)
                .Where(obj => obj.BuildingId == buildingId).ToList();
            return _mapper.Map<IEnumerable<AtticDTO>>(attics);
        }
        public IEnumerable<WallDTO> GetWallsByBuildingId(Guid? buildingId)
        {
            if (buildingId == null || buildingId == Guid.Empty) { return null; }
            IEnumerable<Wall> walls = _dbConnect.Walls.Where(obj => obj.BuildingId == buildingId).ToList();
            return _mapper.Map<IEnumerable<WallDTO>>(walls);
        }
        public IEnumerable<RoofDTO> GetRoofsByBuildingId(Guid? buildingId)
        {
            if (buildingId == null || buildingId == Guid.Empty) { return null; }
            IEnumerable<Roof> roofs = _dbConnect.Roofs
                .Include(obj => obj.Insulations)
                .Include(obj => obj.Skylights)
                .Where(obj => obj.BuildingId == buildingId).ToList();
            return _mapper.Map<IEnumerable<RoofDTO>>(roofs);
        }
        public IEnumerable<SkylightDTO> GetSkylightByRoof(IEnumerable<Roof> roofs)
        {
            if (roofs == null || roofs.Count() == 0) return null;

            return null;
        }
    }
}
