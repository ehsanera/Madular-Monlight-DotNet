using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Module.Role.Application.Dto;
using Module.Role.Domain;
using Module.Role.Persistence;
using Module.Shared.Application;
using Module.Shared.Application.Core;

namespace Module.Role.Application;

public class RoleService : IBaseService<RoleCreateDto, RoleUpdateDto, RoleDto, int>
{
    private readonly IRoleRepository<RoleContext> _roleRepository;
    private readonly IMapper _mapper;

    public RoleService(
        IRoleRepository<RoleContext> roleRepository,
        IMapper mapper
    )
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<Result<RoleDto?>> GetById(
        int id
    )
    {
        var res = await _roleRepository.GetByIdAsync(id);

        return Result<RoleDto?>.CreateResult(
            res is null,
            _mapper.Map(res, new RoleDto()),
            "Role not found"
        );
    }

    public async Task<Result<IEnumerable<RoleDto>>> GetAll()
    {
        var res = await _roleRepository.GetAll().ProjectTo<RoleDto>(_mapper.ConfigurationProvider).ToListAsync();

        return Result<IEnumerable<RoleDto>>.CreateResult(
            res.Count == 0,
            res,
            "No role found"
        );
    }

    public async Task<Result<RoleDto>> AddAsync(
        RoleCreateDto t
    )
    {
        var addingRole = _mapper.Map(t, new Domain.Role());

        await _roleRepository.AddAsync(addingRole);

        return Result<RoleDto>.CreateResult(
            await _roleRepository.SaveChangesAsync() > 0,
            _mapper.Map(addingRole, new RoleDto()),
            "No role added"
        );
    }

    public async Task<Result<RoleDto>> Update(
        int id,
        RoleUpdateDto t
    )
    {
        var entity = _mapper.Map(t, new Domain.Role());

        entity.Id = id;

        var updateResult = await _roleRepository.Update(entity);

        var res = _mapper.Map(updateResult.Entity, new RoleDto());

        return Result<RoleDto>.CreateResult(
            await _roleRepository.SaveChangesAsync() > 0,
            res, 
            "Problem in updating role"
        );
    }

    public async Task<Result<bool>> Delete(
        RoleDto entity
        )
    {
        var deletingRole = _mapper.Map(entity, new Domain.Role());
        
        _roleRepository.Remove(deletingRole);

        return Result<bool>.CreateResult(
            await _roleRepository.SaveChangesAsync() > 0,
            true,
            "Problem in deleting role"
        );
    }
}