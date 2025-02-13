using EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS;
using EnergyScore.Application.Operations;
using EnergyScore.Application.Templates.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneFloorController : ControllerBase
    {
        private readonly IZoneFloorOperatoins _zoneFloorOperations;
        public ZoneFloorController(IZoneFloorOperatoins zoneFloorOperatoins)
        {
            this._zoneFloorOperations = zoneFloorOperatoins;
        }

        // POST api/<ZoneFloorController>
        [HttpPost]
        public ResponseForZoneFloor Post([FromBody] ZoneFloorDTO zoneFloor, [FromQuery] Guid BuildingId)
        {
            ResponseForZoneFloor res = this._zoneFloorOperations.AddZoneFloor(zoneFloor,BuildingId);
            return res;
        }
    }
}
