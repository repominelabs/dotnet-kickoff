using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Repomine.Dotnet.Core.Application.DTOs.Identity.User;
using Repomine.Dotnet.Core.Application.Interfaces.Identity;
using Repomine.Dotnet.Core.Domain.Wrappers;
using Repomine.Dotnet.Infrastructure.Identity.Models;

namespace Repomine.Dotnet.Infrastructure.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<Response<string>> AddUserRoleAsync(AddUserRoleRequest request)
    {
        var result = await _userManager.AddToRoleAsync(_mapper.Map<User>(request), request.Role);
        Response<string> response = new()
        {
            Succeeded = result.Succeeded,
        };

        return response;
    }

    public async Task<Response<string>> DeleteUserAsync(DeleteUserRequest request)
    {
        var result = await _userManager.DeleteAsync(_mapper.Map<User>(request));
        Response<string> response = new()
        {
            Succeeded = result.Succeeded,
        };

        return response;
    }

    public async Task<Response<string>> DeleteUserRoleAsync(DeleteUserRoleRequest request)
    {
        var result = await _userManager.RemoveFromRoleAsync(_mapper.Map<User>(request), request.Role);
        Response<string> response = new()
        {
            Succeeded = result.Succeeded,
        };

        return response;
    }

    public async Task<Response<string>> SaveMultipleUserAsync(SaveMultipleUserRequest request)
    {
        foreach(var user in request.Users)
        {
            await _userManager.CreateAsync(_mapper.Map<User>(user));
        }
        Response<string> response = new();

        return response;
    }

    public async Task<Response<string>> SaveUserAsync(SaveUserRequest request)
    {
        var result = await _userManager.CreateAsync(_mapper.Map<User>(request));
        Response<string> response = new()
        {
            Succeeded = result.Succeeded,
        };

        return response;
    }

    public async Task<Response<SearchUsersResponse>> SearchUsersAsync(SearchUsersRequest request)
    {
        Response<SearchUsersResponse> response = new();
        // Todo : Get Users By PaginatedListAsync
        return response;
    }

    public async Task<Response<string>> UpdateUserAsync(UpdateUserRequest request)
    {
        var result = await _userManager.UpdateAsync(_mapper.Map<User>(request));
        Response<string> response = new()
        {
            Succeeded = result.Succeeded,
        };
        
        return response;
    }
}