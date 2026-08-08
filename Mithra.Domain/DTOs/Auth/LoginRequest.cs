using System.ComponentModel.DataAnnotations;

namespace Mithra.Domain.DTOs.Auth;

public class LoginRequest
{
    [Required] public string Email { get; set; }
    [Required] [EmailAddress] public string Password { get; set; }
}