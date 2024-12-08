using AutoMapper;
using UserMicroservice.Core.API.Models;
using UserMicroservice.Core.API.ViewModels;

namespace UserMicroservice.Core.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUser, User>();
        }
    }
}
