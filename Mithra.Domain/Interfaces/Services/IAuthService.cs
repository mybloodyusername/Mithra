using Mithra.Domain.DTOs.Auth;

namespace Mithra.Domain.Interfaces.Services;

public interface IAuthService
{
    public Task<LoginResponse> Login(LoginRequest request);
    
}