using Microsoft.AspNetCore.Mvc;
using EnergyScore.Application.Operations;
using EnergyScore.Application.Mappers.DTOS;
using EnergyScore.Application.Templates.Responses;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressOperations _AddressOperation;
        private readonly IHPXMLOperations _HPXMLOperations;
        public AddressController(IAddressOperations addressOperation,IHPXMLOperations hPXMLOperations)
        {
            _AddressOperation = addressOperation;
            _HPXMLOperations = hPXMLOperations;
        }

/*        [HttpGet("generate")]
        public ActionResult Get()
        {
            AddressDTO add = _AddressOperation.GetAddress(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));
            _HPXMLOperations.Convertor(add);
            return Ok();
        }*/

        // POST api/<Address>
        [HttpPost]
        public ActionResult<ResponseForAddress> Post([FromBody] AddressDTO req)
        {
            ResponseForAddress res =  _AddressOperation.AddAddress(req);
            return Ok(res);
        }
    }
}