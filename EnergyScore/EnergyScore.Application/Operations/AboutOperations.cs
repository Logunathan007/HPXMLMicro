using AutoMapper;
using EnergyScore.Application.Mappers.DTOS;
using EnergyScore.Application.Templates.Responses;
using EnergyScore.Domain.Entityies;
using EnergyScore.Persistence.DBConnection;

namespace EnergyScore.Application.Operations
{
    public interface IAboutOperations
    {
        public Response AddAbout(AboutDTO aboutDTO,Guid BuildingId);
        public AboutDTO GetAboutById(Guid AboutId);
    }
    public class AboutOperations : IAboutOperations
    {
        private readonly DbConnect _dbConnect;
        private readonly IMapper _mapper;
        private readonly IBuildingOperations _buildingOperations;
        public AboutOperations(DbConnect dbConnect, IMapper mapConfig, IBuildingOperations buildingOperations)
        {
            _dbConnect = dbConnect;
            _mapper = mapConfig;
            _buildingOperations = buildingOperations;
        }

        public Response AddAbout(AboutDTO aboutDTO, Guid BuildingId)
        {
            About about = _mapper.Map<About>(aboutDTO);
            _dbConnect.Abouts.Add(about);
            _dbConnect.SaveChanges();   
            return _buildingOperations.UpdateAboutId(BuildingId, about.Id);
        }

        public AboutDTO GetAboutById(Guid AboutId)
        {
            About about = _dbConnect.Abouts.FirstOrDefault(obj => obj.Id == AboutId);
            if (about == null)
            {
                return null;
            }
            AboutDTO aboutDTO = _mapper.Map<AboutDTO>(about);
            return aboutDTO;
        }

    }
}
