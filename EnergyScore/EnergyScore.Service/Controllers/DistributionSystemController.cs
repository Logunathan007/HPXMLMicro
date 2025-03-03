
using EnergyScore.Application.Mappers.DTOS.DistributionSystemDTOS;
using EnergyScore.Application.Operations;
using EnergyScore.Application.Templates.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributionSystemController : ControllerBase
    {
        private readonly IDistributionSystemOperations _distributionSystemOperations;
        public DistributionSystemController(IDistributionSystemOperations distributionSystemOperations)
        {
            _distributionSystemOperations = distributionSystemOperations;
        }

        [HttpPost]
        public ActionResult<ResponseForDistributionSystem> AddDistribution([FromBody]DistributionSystemsDTO dsDTO, [FromQuery] Guid BuildingID)
        {
            ResponseForDistributionSystem res = _distributionSystemOperations.AddDistributionSystem(dsDTO, BuildingID);
            return Ok(dsDTO);
        }
        [HttpGet("GetNames")]
        public ActionResult<NameWithIds> GetDistributionSystemNameByBuildingId([FromQuery] Guid BuildingId)
        {
            IEnumerable<NameWithId> res = _distributionSystemOperations.GetDistributionSystemNameByBuildingId(BuildingId);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(new NameWithIds() { NamesAndIds= res,Failed=false,Message="Distribution Systems Name and Id successfully send." });
        }
    }
}
