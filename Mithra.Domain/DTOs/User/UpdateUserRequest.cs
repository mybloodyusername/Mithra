using System.ComponentModel.DataAnnotations;

namespace Mithra.Domain.DTOs.User;

public class UpdateUserRequest
{
    [Required] public Guid Id { get; set; }
}