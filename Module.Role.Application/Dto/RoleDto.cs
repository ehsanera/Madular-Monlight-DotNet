using System.ComponentModel.DataAnnotations;
using Module.Shared.Application;

namespace Module.Role.Application.Dto;

public class RoleDto : IBaseDto
{
    public int Id { get; set; }

    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(4000)]
    public string? Description { get; set; }
}