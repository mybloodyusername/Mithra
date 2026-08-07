namespace Mithra.Application.DTOs.User;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}