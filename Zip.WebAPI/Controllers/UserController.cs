using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Zip.WebAPI.Models.Dto;
using Zip.WebAPI.Services;

namespace Zip.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetUsersAsync();
            if (result.IsSuccessful == false)
            {
                return BadRequest(result.ErrorMessages);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (result.IsSuccessful == false)
            {
                return BadRequest(result.ErrorMessages);
            }

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto value)
        {
            var result = await _userService.CreateUserAsync(value);
            if (result.IsSuccessful == false)
            {
                return BadRequest(result.ErrorMessages);
            }

            return Ok(result);
        }

        [HttpPost("Remove")]
        public async Task DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
        }
    }
}
