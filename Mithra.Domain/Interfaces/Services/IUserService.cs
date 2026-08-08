using Mithra.Domain.DTOs.Auth;
using Mithra.Domain.DTOs.User;
using Mithra.Domain.Enums;

namespace Mithra.Domain.Interfaces.Services;

public interface IUserService
{
    public  Task<CreateUpdateUserResponse> Create(CreateUserRequest request, UserRole role);
    public  Task<CreateUpdateUserResponse> Update(UpdateUserRequest request);
}