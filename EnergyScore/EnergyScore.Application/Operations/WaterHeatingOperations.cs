
using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.WaterHeating;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Domain.Entityies.HeatingSystemModels;
using EnergyScore.Persistence.DBConnection;

namespace EnergyScore.Application.Operations
{
    public interface IWaterHeatingOperations
    {
        public ResponseForWaterHeating AddWaterHeating(WaterHeatingDTO waterHeatingDTO, Guid buildingId);
    }
    public class WaterHeatingOperations : IWaterHeatingOperations
    {
        private readonly IBuildingOperations _buildingOperations;
        private readonly IMapper _mapper;
        private readonly DbConnect _dbConnect;
        public WaterHeatingOperations(IBuildingOperations buildingOperations, IMapper mapper, DbConnect dbConnect)
        {
            _buildingOperations = buildingOperations;
            _mapper = mapper;
            _dbConnect = dbConnect;
        }
        public ResponseForWaterHeating AddWaterHeating(WaterHeatingDTO waterHeatingDTO, Guid buildingId)
        {
            WaterHeating waterHeating = _mapper.Map<WaterHeating>(waterHeatingDTO);
            _dbConnect.WaterHeatings.Add(waterHeating);
            _dbConnect.SaveChanges();
            _buildingOperations.UpdateWaterHeatingSystemId(buildingId, waterHeating.Id);
            return new ResponseForWaterHeating { Failed = false, Message = "WaterHeating Added Successfully", WaterHeatingId = waterHeating.Id };
        }
    }
}
