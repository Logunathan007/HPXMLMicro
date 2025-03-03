using EnergyScore.Application.Mappers.DTOS.PhotovoltaicsDTOS;
using EnergyScore.Application.Operations;
using Microsoft.AspNetCore.Mvc;

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotovoltaicController : ControllerBase
    {
        IPhotovoltaicsOperations _photovoltaicsOperations;
        public PhotovoltaicController(IPhotovoltaicsOperations photovoltaicsOperations) 
        {
            _photovoltaicsOperations = photovoltaicsOperations;
        }
        [HttpPost]
        public IActionResult AddPhotovoltaic([FromBody] PhotovoltaicsDTO photovoltaicsDTO, [FromQuery]Guid buildingId)
        {
            var response = _photovoltaicsOperations.Addphotovoltaic(photovoltaicsDTO, buildingId);
            if (response.Failed)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
