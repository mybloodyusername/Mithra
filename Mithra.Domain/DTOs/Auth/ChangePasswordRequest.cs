using System.ComponentModel.DataAnnotations;

namespace Mithra.Domain.DTOs.Auth;

public record ChangePasswordRequest
{
  [Required] public Guid Id;
  [Required] public required string Password;
  [Required] public required string NewPassword;
};