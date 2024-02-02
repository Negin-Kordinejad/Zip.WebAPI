using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.WebAPI.Models;
using Zip.WebAPI.Models.Dto;
using Zip.WebAPI.Models.Enums;
using Zip.WebAPI.Models.Responses;
using Zip.WebAPI.Repository;

namespace Zip.WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Response<UserDto>> CreateUserAsync(UserDto userDto)
        {
            var response = new Response<UserDto>();
            if (userDto == null)
            {
                _logger.LogError("UserService-CreateUserAsync : ");
                throw new ArgumentException("Ussser is not provided");
            }
            var user = _mapper.Map<User>(userDto);
            var result = await _userRepository.CreateAsync(user);

            if (result == null)
            {
                _logger.LogError("UserService-GetUsersAsync : No user found");
                response.AddError(ResponseCode.InternalError.ToString(), "User has not created");
                return response;
            }

            response.Data = _mapper.Map<UserDto>(result);

            return response;
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<Response<UserDto>> GetUserByIdAsync(int id)
        {
            var response = new Response<UserDto>();
            var result = await _userRepository.GetByIdAsync(id);
            if (result == null)
            {
                _logger.LogError("UserService-GetUsersAsync : No user found");
                response.AddError(ResponseCode.NotFound.ToString(), "No user found");
            }
            else
            {
                response.Data = _mapper.Map<UserDto>(result);
            }

            return response;
        }

        public async Task<Response<List<UserDto>>> GetUsersAsync()
        {
            var response = new Response<List<UserDto>>();
            var result = await _userRepository.GetUsersAsync();
            if (result == null)
            {
                _logger.LogError("UserService-GetUsersAsync : No user found");
                response.AddError(ResponseCode.NotFound.ToString(), "No user found");
            }
            else
            {
                response.Data = _mapper.Map<List<UserDto>>(result);
            }

            return response;
        }
    }
}
