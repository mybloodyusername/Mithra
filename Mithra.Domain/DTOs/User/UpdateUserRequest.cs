using System.ComponentModel.DataAnnotations;

namespace Mithra.Domain.DTOs.User;

public class UpdateUserRequest
{
    [Required] public Guid Id { get; set; }
    [MaxLength(64)] public string? FirstName { get; set; }
    [MaxLength(64)] public string? LastName { get; set; }
}