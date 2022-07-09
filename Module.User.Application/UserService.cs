using AutoMapper;
using AutoMapper.QueryableExtensions;
using Module.Shared.Application;
using Module.Shared.Application.Core;
using Module.User.Application.Dto;
using Module.User.Domain;
using Module.User.Persistence;

namespace Module.User.Application;

public class UserService : IBaseService<UserCreateDto, UserUpdateDto, UserDto, int>
{
    private readonly IUserRepository<UserContext> _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository<UserContext> userRepository,
        IMapper mapper
    )
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDto?>> GetById(
        int id
    )
    {
        var res = await _userRepository.GetByIdAsync(id);

        return Result<UserDto?>.CreateResult(
            res is null,
            _mapper.Map(res, new UserDto()),
            "User not found"
        );
    }

    public async Task<Result<Paginator<UserDto>>> GetAll(
        int currentPageNumber,
        int itemsCountPerPage
    )
    {
        var res = await _userRepository.GetAll()
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .Paginate(
                currentPageNumber,
                itemsCountPerPage
            );

        return Result<Paginator<UserDto>>.CreateResult(
            res.Count == 0,
            res,
            "No user found"
        );
    }

    public async Task<Result<UserDto>> AddAsync(
        UserCreateDto t
    )
    {
        var addingUser = _mapper.Map(t, new Domain.User());

        await _userRepository.AddAsync(addingUser);

        return Result<UserDto>.CreateResult(
            await _userRepository.SaveChangesAsync() > 0,
            _mapper.Map(addingUser, new UserDto()),
            "No user added"
        );
    }

    public async Task<Result<UserDto>> Update(
        int id,
        UserUpdateDto t
    )
    {
        var entity = _mapper.Map(t, new Domain.User());

        entity.Id = id;

        var updateResult = await _userRepository.Update(entity);

        var res = _mapper.Map(updateResult.Entity, new UserDto());

        return Result<UserDto>.CreateResult(
            await _userRepository.SaveChangesAsync() > 0,
            res,
            "Problem in updating user"
        );
    }

    public async Task<Result<bool>> Delete(
        UserDto entity
    )
    {
        var deletingRole = _mapper.Map(entity, new Domain.User());

        _userRepository.Remove(deletingRole);

        return Result<bool>.CreateResult(
            await _userRepository.SaveChangesAsync() > 0,
            true,
            "Problem in deleting user"
        );
    }
}