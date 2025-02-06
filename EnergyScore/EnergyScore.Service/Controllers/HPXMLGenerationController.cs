using EnergyScore.Application.Operations;
using EnergyScore.Application.Templates.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergyScore.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HPXMLGenerationController : ControllerBase
    {
        private readonly IHPXMLGenerationOperations _hPXMLGenerationOperations;
        public HPXMLGenerationController(IHPXMLGenerationOperations hPXMLGenerationOperations)
        {
            _hPXMLGenerationOperations = hPXMLGenerationOperations;
        }

        [HttpGet("string")]
        public ActionResult<HPXMLStringResponse> GetString([FromQuery] string BuildingId)
        {

            HPXMLGenerationResponse res = _hPXMLGenerationOperations.GenerateHpxml(new Guid(BuildingId));
            if(res.Failed)
            {
                return new HPXMLStringResponse { Failed = true, Message = res.Message };
            }
            string hpxmlString = _hPXMLGenerationOperations.GenerateHPXMLString(res.hPXML);
            return new HPXMLStringResponse { Failed = false, HPXMLString = hpxmlString };
        }

        [HttpGet("base64")]
        public ActionResult<HPXMLBase64Response> getBase64String([FromQuery] string BuildingId)
        {
            HPXMLGenerationResponse res = _hPXMLGenerationOperations.GenerateHpxml(new Guid(BuildingId));
            if (res.Failed)
            {
                return new HPXMLBase64Response { Failed = true, Message = res.Message };
            }
            string hpxmlString = _hPXMLGenerationOperations.GenerateHPXMLStringBase64Encode(res.hPXML);
            return new HPXMLBase64Response { Failed = false, HPXMLBase64String = hpxmlString };
        }
    }
}
