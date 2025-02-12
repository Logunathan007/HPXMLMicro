using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneFloorController : ControllerBase
    {

        // POST api/<ZoneFloorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
