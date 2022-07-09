using System.ComponentModel.DataAnnotations;
using Module.Shared.Application;

namespace Module.Role.Application.Dto;

public class RoleCreateDto : IBaseCreateDto
{
    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(4000)]
    public string? Description { get; set; }
}