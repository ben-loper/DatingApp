using API.Models;
using AutoMapper;
using Database.Data;

namespace API.Profiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<AppUser, UserDto>();
        }
    }
}
