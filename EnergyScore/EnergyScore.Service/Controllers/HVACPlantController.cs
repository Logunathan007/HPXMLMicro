using EnergyScore.Application.Mappers.DTOS.HVACPlantDTOS;
using EnergyScore.Application.Operations;
using EnergyScore.Application.Templates.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HVACPlantController : ControllerBase
    {
        private readonly IHVACPlantOperations _hvacPlantOperations;
        public HVACPlantController(IHVACPlantOperations hVACPlantOperations)
        {
            _hvacPlantOperations = hVACPlantOperations;
        }
        [HttpPost]
        public ActionResult<ResponseForHVACPlant> AddHVACPlant([FromBody] HVACPlantDTO hvacPlantDTO, [FromQuery] Guid BuildingId)
        {
            ResponseForHVACPlant response = _hvacPlantOperations.AddHVACPlant(hvacPlantDTO, BuildingId);
            if (response.Failed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
