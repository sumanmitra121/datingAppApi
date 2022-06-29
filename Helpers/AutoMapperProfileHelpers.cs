
using AutoMapper;
using TestApi.DTOs;
using TestApi.Entities;
using TestApi.Extensions;

namespace TestApi.Helpers
{
    public class AutoMapperProfileHelpers : Profile
    {
        public AutoMapperProfileHelpers()
        {
             CreateMap<AppUser,MemeberDto>().ForMember(dest=>dest.photoUrl,opt=> opt.MapFrom(x=>x.photos.FirstOrDefault(x=> x.isMain).url))
             .ForMember(dest => dest.age,opt=> opt.MapFrom(x=>x.dateOfbirth.CalculateAge()));
             CreateMap<photo,photoDTO>();

        }
    }
}