using AutoMapper;
using Todo.Api.DTOs;
using Todo.Api.DTOs.Identity;
using Todo.Domain.Entities;
using Todo.Domain.Entities.Identity;

namespace Todo.Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoEntityDto, TodoEntity>()
                       .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.Completed))
                       .ReverseMap()
                       .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.IsCompleted));
            CreateMap<LoginDto, ApplicationUser>().ReverseMap();
            CreateMap<LoginResultDto, ApplicationUser>().ReverseMap();
            CreateMap<UserDto, ApplicationUser>().ReverseMap();

        }
    }
}
