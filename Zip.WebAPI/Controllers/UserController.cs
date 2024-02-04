﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Zip.WebAPI.Models.Dto;
using Zip.WebAPI.Models.Enums;
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
            var response = await _userService.GetUsersAsync();
            if (response.IsSuccessful == false)
            {
                string errorCode = response.ErrorMessages[0].ErrorCode;

                if (errorCode == ResponseCode.NotFound.ToString())
                {
                    return NotFound(response.ErrorMessages);
                }

            }
            return Ok(response);
        }

        [HttpGet("{emailAddress}")]
        public async Task<IActionResult> GetByEmailAddressId(string emailAddress)
        {
            var response = await _userService.GetUserByEmailAsync(emailAddress);
            if (response.IsSuccessful == false)
            {
                string errorCode = response.ErrorMessages[0].ErrorCode;

                if (errorCode == ResponseCode.NotFound.ToString())
                {
                    return NotFound(response.ErrorMessages);
                }

                return BadRequest(response.ErrorMessages);
            }

            return Ok(response);
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
        public async Task DeleteUser(string emailAddress)
        {
            await _userService.DeleteUserAsync(emailAddress);
        }
    }
}
