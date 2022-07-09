using Module.Shared.Application;

namespace Module.User.Application.Dto;

public class UserCreateDto : IBaseCreateDto
{
    public string Name { get; set; }
}