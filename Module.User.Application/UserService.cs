using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Module.Shared.Application;
using Module.User.Application.Dto;
using Module.User.Domain;

namespace Module.User.Application;

public class UserService : IBaseService<UserCreateDto, UserUpdateDto, UserDto, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
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
        await _userRepository.SaveAsync();
        return userDto;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}