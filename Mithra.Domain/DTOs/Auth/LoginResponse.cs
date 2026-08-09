namespace Mithra.Domain.DTOs.Auth;

public record LoginResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}