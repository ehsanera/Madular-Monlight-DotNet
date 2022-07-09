using Module.Shared.Application;

namespace Module.User.Application.Dto;

public class UserUpdateDto : IBaseUpdateDto
{
    public string Name { get; set; }
}