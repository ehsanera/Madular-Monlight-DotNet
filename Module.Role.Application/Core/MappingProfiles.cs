using AutoMapper;
using Module.Role.Application.Dto;

namespace Module.Role.Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Role, RoleDto>();

        CreateMap<RoleCreateDto, Domain.Role>();
    }
}