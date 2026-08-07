using System.Security.Authentication;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Mithra.Application.DTOs.Auth;
using Mithra.Application.Interfaces.Services;
using Mithra.Domain.Entities;

namespace Mithra.Application.Services;

public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    : IAuthService
{
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var applicationUser = await userManager.FindByEmailAsync(request.Email);
        if (applicationUser == null)
            throw new InvalidCredentialException();

        var signInResult = await signInManager.PasswordSignInAsync(applicationUser, request.Password, false, false);

        return !signInResult.Succeeded
            ? throw new InvalidCredentialException()
            : applicationUser.Adapt<LoginResponse>();
    }
}