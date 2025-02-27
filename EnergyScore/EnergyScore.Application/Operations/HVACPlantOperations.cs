
using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.HVACPlantDTOS;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Domain.Entityies.HVACPlantModels;
using EnergyScore.Persistence.DBConnection;

namespace EnergyScore.Application.Operations
{
    public interface IHVACPlantOperations
    {
        public ResponseForHVACPlant AddHVACPlant(HVACPlantDTO hvacPlantDTO, Guid BuildingId);

    }
    public class HVACPlantOperations : IHVACPlantOperations
    {
        private readonly DbConnect _dbConnect;
        private readonly IBuildingOperations _buildingOperations;
        private readonly IMapper _mapper;
        public HVACPlantOperations(DbConnect dbConnect,IBuildingOperations buildingOperations,IMapper mapper)
        {
            _dbConnect = dbConnect;
            _buildingOperations = buildingOperations;
            _mapper = mapper;
        }
        public ResponseForHVACPlant AddHVACPlant(HVACPlantDTO hvacPlantDTO, Guid BuildingId)
        {
            HVACPlant hvacPlant = _mapper.Map<HVACPlant>(hvacPlantDTO);
            if(hvacPlant == null) return new ResponseForHVACPlant() { Failed = true, Message = "HVAC Plant not srored" };
            _dbConnect.HVACPlants.Add(hvacPlant);
            _dbConnect.SaveChanges();
            _buildingOperations.UpdateHVACPlantId(BuildingId, hvacPlant.Id);
            return new ResponseForHVACPlant() { Failed = false, Message = "HVAC Plant added succesfully", HVACPlantID = hvacPlant.Id };
        }
    }
}
