using AutoMapper;
using Zip.WebAPI.Models;
using Zip.WebAPI.Models.Dto;

namespace Zip.WebAPI.MappingProfiles
{
    public class AcountDtoMapper : Profile
    {
        public AcountDtoMapper()
        {
            CreateMap<Acount, AcountDto>();
            CreateMap<AcountDto, Acount>().ForMember(dest => dest.User, opt => opt.Ignore());
            CreateMap<Acount, AcountTypeDto>();
        }
    }
}
