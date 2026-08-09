using System.ComponentModel.DataAnnotations;

namespace Mithra.Domain.DTOs.Auth;

public record RegisterUserRequest
{
    [Required] [EmailAddress] public required string Email { get; set; }
    [Required] public required string Password { get; set; }
}