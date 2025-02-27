
using AutoMapper;
using EnergyScore.Application.Mappers.DTOS.PhotovoltaicsDTOS;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Domain.Entityies.PhotovoltaicsModels;
using EnergyScore.Persistence.DBConnection;
using Microsoft.EntityFrameworkCore;

namespace EnergyScore.Application.Operations
{
    public interface IPhotovoltaicsOperations
    {
        public ResponseForPhotovoltaic Addphotovoltaic(PhotovoltaicsDTO photovoltaicsDTO, Guid buildingId);
    }
    public class PhotovoltaicsOperations : IPhotovoltaicsOperations
    {
        private readonly IBuildingOperations _buildingOperations;
        private readonly IMapper _mapper;
        private readonly DbConnect _dbConnect;
        public PhotovoltaicsOperations(DbConnect dbConnect, IMapper mapper, IBuildingOperations buildingOperations)
        {
            _dbConnect = dbConnect;
            _mapper = mapper;
            _buildingOperations = buildingOperations;
        }
        public ResponseForPhotovoltaic Addphotovoltaic(PhotovoltaicsDTO photovoltaicsDTO, Guid buildingId)
        {
            Photovoltaics photovoltaic = _mapper.Map<Photovoltaics>(photovoltaicsDTO);
            _dbConnect.Add(photovoltaic);
            _dbConnect.SaveChanges();
            _buildingOperations.UpdatePhotovoltaicsId(buildingId, photovoltaic.Id);
            return new ResponseForPhotovoltaic { Failed = false, Message = "Photovoltaics Added Successfully",PhotovoltaicsID  = photovoltaic.Id };
        }
    }
}
