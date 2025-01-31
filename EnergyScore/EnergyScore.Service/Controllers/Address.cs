using Microsoft.AspNetCore.Mvc;
using EnergyScore.Application.Operations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Address : ControllerBase
    {
        private readonly AddressOperations _AddressOperation;
        public Address(AddressOperations addressOperation)
        {
            _AddressOperation = addressOperation;
        }

        // POST api/<Address>
        [HttpPost]
        public ActionResult Post([FromBody] Domain.Entityies.Address req)
        {
            _AddressOperation.AddAddress(req);  
            return Ok(200);
        }

    }
}
