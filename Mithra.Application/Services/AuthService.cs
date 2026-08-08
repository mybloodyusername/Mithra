using System.Security.Authentication;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Mithra.Domain.DTOs.Auth;
using Mithra.Domain.Entities;
using Mithra.Domain.Enums;
using Mithra.Domain.Exceptions;
using Mithra.Domain.Interfaces.Services;

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

    public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            throw new ConflictException("User with this email already exists.");

        var applicationUser = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
        };

        var result = await userManager.CreateAsync(applicationUser, request.Password);

        if (!result.Succeeded) throw new InvalidOperationException("Failed to create user.");

        var roleResult = await userManager.AddToRoleAsync(applicationUser, nameof(UserRole.User));

        if (!roleResult.Succeeded) throw new InvalidOperationException("Failed to create user role.");

        return applicationUser.Adapt<RegisterUserResponse>();
    }
}