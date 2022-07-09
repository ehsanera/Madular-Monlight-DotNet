using Module.Shared.Application;

namespace Module.User.Application.Dto;

public class UserDto : IBaseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}