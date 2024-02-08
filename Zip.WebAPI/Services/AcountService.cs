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
    public class AcountService : IAcountService
    {
        private readonly ILogger<AcountService> _logger;
        private readonly IMapper _mapper;
        private readonly IAcountRepository _acountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICreditValidator _creditValidator;

        public AcountService(ILogger<AcountService> logger, IMapper mapper, IAcountRepository acountRepository, IUserRepository userRepository, ICreditValidator creditValidator)
        {
            _logger = logger;
            _mapper = mapper;
            _acountRepository = acountRepository;
            _userRepository = userRepository;
            _creditValidator = creditValidator;
        }

        public async Task<Response<AcountDto>> CreateAcountAsync(AcountCreateDto acountDto)
        {
            var response = new Response<AcountDto>();
            if (acountDto == null)
            {
                _logger.LogError("AcountService-CreateAcountAsync : ");
                throw new ArgumentException("Acount is not provided");
            }
            var user = await _userRepository.GetByEmailAsync(acountDto.Email);
            if (user == null)
            {
                _logger.LogError($"AcountService-CreateAcountAsync : No user found to create anacount for user {acountDto.Email}.");
                response.AddError(ResponseCode.InternalError.ToString(), " No user found to create anacount.");
                return response;
            }
            var responseValidator = await _creditValidator.ValidateAsync(user);
            if (responseValidator == null)
            {
                _logger.LogError($"AcountService-CreateAcountAsync : Issue in validating for user {acountDto.Email}.");
                response.AddError(ResponseCode.InternalError.ToString(), " Inernal issue");
                return response;
            }

            if (!responseValidator.IsValid)
            {
                _logger.LogError($"AcountService-CreateAcountAsync : User is not valid for creating an acount for user {acountDto.Email}. ");
                response.AddError(ResponseCode.BadRequest.ToString(), "User has no credit to make an acount.");
                return response;
            }
            var acount = new Acount { UserId = user.Id, Type = acountDto.Type };
            var result = await _acountRepository.CreateAsync(acount);

            if (result == null)
            {
                _logger.LogError($"AcountService-CreateAcountAsync : No acount has created for user {acountDto.Email}.");
                response.AddError(ResponseCode.InternalError.ToString(), "No acount has created");
                return response;
            }

            response.Data = _mapper.Map<AcountDto>(result);

            return response;
        }

        public async Task<Response<List<AcountDto>>> GetAcountsByEmailAddressAsync(string email)
        {
            var response = new Response<List<AcountDto>>();
            var result = await _acountRepository.GetByUserEmailAsync(email);
            if (result == null)
            {
                _logger.LogError("AcountService-GetAcountsByEmailAddressAsync : No acount has found");
                response.AddError(ResponseCode.NotFound.ToString(), "No acount has found");
            }
            else
            {
                response.Data = _mapper.Map<List<AcountDto>>(result);
            }

            return response;
        }


    }
}
