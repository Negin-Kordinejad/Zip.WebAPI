using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Zip.WebAPI.Models.Dto;
using Zip.WebAPI.Models.Enums;
using Zip.WebAPI.Services;


namespace Zip.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        private readonly ILogger<AcountController> _logger;
        private readonly IAcountService _acountService;

        public AcountController(ILogger<AcountController> logger, IAcountService acountService)
        {
            _logger = logger;
            _acountService = acountService;
        }

        [HttpGet("{emailAddress}")]
        public async Task<IActionResult> GetByUserId(string emailAddress)
        {
            var response = await _acountService.GetAcountsByEmailAddressAsync(emailAddress);
            if (!response.IsSuccessful)
            {
                string errorCode = response.ErrorMessages[0].ErrorCode;

                if (errorCode == ResponseCode.NotFound.ToString())
                {
                    return NotFound(response.ErrorMessages);
                }

                if (errorCode == ResponseCode.BadRequest.ToString())
                {
                    return BadRequest(response.ErrorMessages);
                }
            }

            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] AcountCreateDto acount)
        {
            var response = await _acountService.CreateAcountAsync(acount);
            if (!response.IsSuccessful)
            {
                string errorCode = response.ErrorMessages[0].ErrorCode;

                if (errorCode == ResponseCode.NotFound.ToString())
                {
                    return NotFound(response.ErrorMessages);
                }
                if (errorCode == ResponseCode.BadRequest.ToString())
                {
                    return BadRequest(response.ErrorMessages);
                }
            }

            return Ok(response);
        }
    }
}

