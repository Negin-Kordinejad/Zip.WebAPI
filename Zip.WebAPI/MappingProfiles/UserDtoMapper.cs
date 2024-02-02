using AutoMapper;
using Zip.WebAPI.Models;
using Zip.WebAPI.Models.Dto;

namespace Zip.WebAPI.MappingProfiles
{
    public class UserDtoMapper : Profile
    {
        public UserDtoMapper()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>().ForMember(dest => dest.Acounts, opt => opt.Ignore()); ;
        }
    }
}
