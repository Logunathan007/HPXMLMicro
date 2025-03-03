using EnergyScore.Application.Mappers.DTOS.WaterHeating;
using EnergyScore.Application.Operations;
using EnergyScore.Application.Templates.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterHeatingController : ControllerBase
    {
        private readonly IWaterHeatingOperations _waterHeatingOperations;
        public WaterHeatingController(IWaterHeatingOperations waterHeatingOperations)
        {
            _waterHeatingOperations = waterHeatingOperations;
        }
        [HttpPost]
        public ActionResult<ResponseForWaterHeating> CreateWaterHeating([FromBody] WaterHeatingDTO waterHeatingDTO, [FromQuery] Guid BuildingId)
        {
            ResponseForWaterHeating response = _waterHeatingOperations.AddWaterHeating(waterHeatingDTO, BuildingId);
            if (response.Failed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
