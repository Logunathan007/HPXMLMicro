
using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.DistributionSystemDTOS;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Domain.Entityies.DistributionSystemModels;
using EnergyScore.Persistence.DBConnection;

namespace EnergyScore.Application.Operations
{
    public interface IDistributionSystemOperations
    {
        public ResponseForDistributionSystem AddDistributionSystem(DistributionSystemsDTO dsDTO, Guid BuildingId);
        public IEnumerable<NameWithId>? GetDistributionSystemNameByBuildingId(Guid? BuildingId);
    }
    public class DistributionSystemOperations : IDistributionSystemOperations
    {
        private readonly DbConnect _dbConnect;
        private readonly IMapper _mapper;
        private readonly IBuildingOperations _buildingOperations;
        public DistributionSystemOperations(DbConnect dbConnect, IMapper mapper, IBuildingOperations buildingOperations)
        {
            _dbConnect = dbConnect;
            _mapper = mapper;
            _buildingOperations = buildingOperations;
        }

        public ResponseForDistributionSystem AddDistributionSystem(DistributionSystemsDTO dsDTO, Guid BuildingId)
        {
            DistributionSystems ds = _mapper.Map<DistributionSystems>(dsDTO);
            if(ds == null) return new ResponseForDistributionSystem() { Failed = true, Message = "Distribution System not srored" };
            _dbConnect.DistributionSystems.Add(ds);
            _dbConnect.SaveChanges();
            _buildingOperations.UpdateDistributionSystemId(BuildingId, ds.Id);
            return new ResponseForDistributionSystem() { Failed = false, Message = "Distribution System added succesfully", DistributionSystemID = ds.Id };
        }
        public IEnumerable<DistributionSystemDTO> GetDistributionSystemByBuildingId(Guid? BuildingId)
        {
            if (BuildingId == Guid.Empty || BuildingId == null) { return null; }
            ICollection<DistributionSystem> ds = _dbConnect.DistributionSystem
                .Where(obj => obj.BuildingId == BuildingId).ToList();
            return _mapper.Map<IEnumerable<DistributionSystemDTO>>(ds);
        }
        public IEnumerable<NameWithId>? GetDistributionSystemNameByBuildingId(Guid? BuildingId)
        {
            if (BuildingId == null || BuildingId == Guid.Empty)
            {
                return null;
            }
            IEnumerable<NameWithId> val = _dbConnect.DistributionSystem
                .Where(obj => obj.BuildingId == BuildingId)
                .Select(obj => new NameWithId() { Name = obj.DistributionSystemName, Id = obj.Id })
                .ToList();
            return val.Count() == 0 ? null : val;
        }
    }
}
