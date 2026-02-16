using AutoMapper;
using WebApplication4.DTOs;
using WebApplication4.Models;

namespace WebApplication4.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<User, UserDto>();
        }
    }
}
