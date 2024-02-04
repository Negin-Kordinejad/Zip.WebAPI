using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.WebAPI.Extentions;
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
                throw new ArgumentException("User is not provided");
            }
            if (string.IsNullOrEmpty(userDto.Email) || !userDto.Email.IsValidEmailAddress())
            {
                throw new ArgumentException("Email address is incorrect.");
            }
            var user = _mapper.Map<User>(userDto);

            var result = await _userRepository.CreateAsync(user);
            if (result == null)
            {
                _logger.LogError($"UserService-GetUsersAsync : User has not created for email {userDto.Email}");
                response.AddError(ResponseCode.InternalError.ToString(), "User has not created");
                return response;
            }

            response.Data = _mapper.Map<UserDto>(result);

            return response;
        }

        public async Task DeleteUserAsync(string email)
        {
            if (String.IsNullOrEmpty(email) || !email.IsValidEmailAddress())
            {
                throw new ArgumentException("Email address is incorrect.");
            }

            await _userRepository.DeleteAsync(email);
        }

        public async Task<Response<UserAcountDto>> GetUserByEmailAsync(string email)
        {
            var response = new Response<UserAcountDto>();
            if (string.IsNullOrEmpty(email) || !email.IsValidEmailAddress())
            {
                throw new ArgumentException("Email address is incorrect.");
            }
            var result = await _userRepository.GetByEmailAsync(email);
            if (result == null)
            {
                _logger.LogError("UserService-GetUsersAsync : No user found");
                response.AddError(ResponseCode.NotFound.ToString(), "No user found");
            }
            else
            {
                response.Data = GetUserAcount(result);
            }

            return response;
        }

        public async Task<Response<List<UserDto>>> GetUsersAsync()
        {
            var response = new Response<List<UserDto>>();
            var result = await _userRepository.GetAllAsync();
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

        private UserAcountDto GetUserAcount(User result)
        {
            return new UserAcountDto
            {
                Name = result.Name,
                Email = result.Email,
                Acounts = _mapper.Map<List<AcountTypeDto>>(result.Acounts)
            };
        }
    }
}
