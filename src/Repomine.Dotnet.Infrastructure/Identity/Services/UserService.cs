using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repomine.Dotnet.Core.Application.DTOs.Identity.User;
using Repomine.Dotnet.Core.Application.Interfaces.Identity;
using Repomine.Dotnet.Core.Domain.Wrappers;
using Repomine.Dotnet.Infrastructure.Identity.Models;

namespace Repomine.Dotnet.Infrastructure.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public Task<Response<AddUserRoleResponse>> AddUserRole(AddUserRoleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<DeleteUserResponse>> DeleteUser(DeleteUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<DeleteUserRoleResponse>> DeleteUserRole(DeleteUserRoleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<SaveMultipleUserResponse>> SaveMultipleUser(SaveMultipleUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<SaveUserResponse>> SaveUser(SaveUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<SearchUsersResponse>> SearchUsers(SearchUsersRequest request)
    {
        // await _userManager.Users.ToListAsync();
        throw new NotImplementedException();
    }

    public Task<Response<UpdateUserResponse>> UpdateUser(UpdateUserRequest request)
    {
        throw new NotImplementedException();
    }
}