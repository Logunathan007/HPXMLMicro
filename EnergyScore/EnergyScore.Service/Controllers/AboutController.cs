using EnergyScore.Application.Mappers.DTOS;
using EnergyScore.Application.Operations;
using EnergyScore.Application.Templates.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutOperations _aboutOperations;
        public AboutController(IAboutOperations aboutOperations)
        {
            _aboutOperations = aboutOperations;
        }

        // POST api/<AboutController>
        [HttpPost]
        public ActionResult<Response> Post([FromBody] AboutDTO add, [FromQuery] Guid BuildingId)
        {
            return Ok(_aboutOperations.AddAbout(add,BuildingId));
        }
    }
}
