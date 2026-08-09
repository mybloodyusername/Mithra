namespace Mithra.Domain.DTOs.Auth;

public record RegisterUserResponse
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}