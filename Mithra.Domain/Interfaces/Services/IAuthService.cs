using Microsoft.AspNetCore.Identity.Data;
using Mithra.Domain.DTOs.Auth;
using LoginRequest = Mithra.Domain.DTOs.Auth.LoginRequest;

namespace Mithra.Domain.Interfaces.Services;

public interface IAuthService
{
    public Task<LoginResponse> Login(LoginRequest request);

    public Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
}