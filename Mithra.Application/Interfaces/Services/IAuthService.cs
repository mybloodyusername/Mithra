using Microsoft.AspNetCore.Mvc;
using Mithra.Application.DTOs.Auth;

namespace Mithra.Application.Interfaces.Services;

public interface IAuthService
{
    public Task<LoginResponse> Login(LoginRequest request);
}