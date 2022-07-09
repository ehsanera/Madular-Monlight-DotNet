using AutoMapper;
using Module.User.Application.Dto;

namespace Module.User.Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.User, UserDto>();
        CreateMap<UserCreateDto, Domain.User>();
        CreateMap<UserUpdateDto, Domain.User>();
    }
}