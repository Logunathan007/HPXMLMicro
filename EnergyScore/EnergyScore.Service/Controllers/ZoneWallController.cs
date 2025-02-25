using EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS;
using EnergyScore.Application.Mappers.DTOS.ZoneWallDTOS;
using EnergyScore.Application.Operations;
using EnergyScore.Application.Templates.HPXMLs;
using EnergyScore.Application.Templates.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneWallController : ControllerBase
    {
        private readonly IZoneWallOperations _zoneWallOperations;
        public ZoneWallController(IZoneWallOperations zoneWallOperations)
        {
            _zoneWallOperations = zoneWallOperations;
        }
        [HttpPost]
        public ActionResult<ResponseForZoneWall> Post([FromBody] ZoneWallDTO zoneWallDTO, [FromQuery] Guid BuildingId)
        {
            if (zoneWallDTO == null)
            {
                return BadRequest();
            }
            ResponseForZoneWall responseForZoneWall = _zoneWallOperations.AddZoneWall(zoneWallDTO, BuildingId);
            return Ok(responseForZoneWall);
        }
    }
}
