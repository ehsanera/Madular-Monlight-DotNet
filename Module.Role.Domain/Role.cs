using System.ComponentModel.DataAnnotations;

namespace Module.Role.Domain;

public class Role
{
    public int Id { get; set; }

    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(4000)]
    public string? Description { get; set; }
}