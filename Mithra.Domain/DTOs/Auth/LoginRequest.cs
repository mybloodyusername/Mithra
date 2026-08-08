using System.ComponentModel.DataAnnotations;

namespace Mithra.Domain.DTOs.Auth;

public class LoginRequest
{
    [Required] public required string Email { get; set; }
    [Required] [EmailAddress] public required string Password { get; set; }
}