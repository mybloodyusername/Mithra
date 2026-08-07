using System.ComponentModel.DataAnnotations;

namespace Mithra.Application.DTOs.User;

public class UpdateUserRequest
{
    [Required] public Guid Id { get; set; }
}