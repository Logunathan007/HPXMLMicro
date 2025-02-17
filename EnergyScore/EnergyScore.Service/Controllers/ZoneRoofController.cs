using EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS;
using EnergyScore.Application.Operations;
using EnergyScore.Application.Templates.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneRoofController : ControllerBase
    {
        private readonly IZoneRoofOperations _zoneRoofOperations;
        public ZoneRoofController(IZoneRoofOperations zoneRoofOperations)
        {
            _zoneRoofOperations = zoneRoofOperations;
        }
        [HttpPost]
        public ActionResult<ZoneRoofDTO> Post([FromBody] ZoneRoofDTO zoneRoofDTO, [FromQuery] Guid buildingID)
        {
            if (zoneRoofDTO == null)
            {
                return BadRequest();
            }
            ResponseForZoneRoof responseForZoneRoof = _zoneRoofOperations.AddZoneRoof(zoneRoofDTO, buildingID);
            return Ok(responseForZoneRoof);
        }
    }
}
