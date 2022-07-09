using AutoMapper;
using Module.Shared.Application;
using Module.User.Application.Dto;
using Module.User.Domain;
using Module.User.Persistence;

namespace Module.User.Application;

public class UserService : IBaseService<UserCreateDto, UserUpdateDto, UserDto, int>
{
    private readonly IUserRepository<UserContext> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository<UserContext> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetById(int id)
    {
        return _mapper.Map(await _userRepository.GetByIdAsync(id), new UserDto());
    }

    public IEnumerable<UserDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> AddAsync(UserCreateDto t)
    {
        return _mapper.Map(
            await _userRepository.AddAsync(
                _mapper.Map(t, new Domain.User())
            ), new UserDto()
        );
    }

    public async Task<UserDto> Update(int id, UserUpdateDto t)
    {
        var entity = _mapper.Map(t, new Domain.User());
        entity.Id = id;
        var userDto = _mapper.Map(
            _userRepository.Update(entity),
            new UserDto()
        );
        await _userRepository.SaveChangesAsync();
        return userDto;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}