using System.ComponentModel.DataAnnotations;

namespace Mithra.Domain.DTOs.User;

public class CreateUserRequest
{
    [MaxLength(64)] public string? FirstName { get; set; }
    [MaxLength(64)] public string? LastName { get; set; }
    [Required] [EmailAddress] public required string Email { get; set; }
    [Required] public required string Password { get; set; }
}