using Mapster;
using Microsoft.AspNetCore.Identity;
using Mithra.Domain.DTOs.Auth;
using Mithra.Domain.DTOs.User;
using Mithra.Domain.Entities;
using Mithra.Domain.Enums;
using Mithra.Domain.Exceptions;
using Mithra.Domain.Interfaces.Services;

namespace Mithra.Application.Services;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<CreateUpdateUserResponse> Create(CreateUserRequest request, UserRole role)
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

        if (!result.Succeeded) throw new ConflictException("Failed to create user.");

        var roleResult = await userManager.AddToRoleAsync(applicationUser, nameof(role));

        if (!roleResult.Succeeded) throw new ConflictException("Failed to create user role.");

        return applicationUser.Adapt<CreateUpdateUserResponse>();
    }

    public async Task<CreateUpdateUserResponse> Update(UpdateUserRequest request)
    {
        var existingUser = await userManager.FindByIdAsync(request.Id.ToString());
        if (existingUser == null) throw new NotFoundException("User with this id not found.");

        existingUser.FirstName = request.FirstName ?? string.Empty;
        existingUser.LastName = request.LastName ?? string.Empty;
        existingUser.UpdatedAt = DateTimeOffset.UtcNow;

        var result = await userManager.UpdateAsync(existingUser);

        if (!result.Succeeded) throw new ConflictException("Failed to update user.");
        return existingUser.Adapt<CreateUpdateUserResponse>();
    }

    public Task<LoginResponse> ChangePassword(LoginRequest request)
    {
        throw new NotImplementedException();
    }
}