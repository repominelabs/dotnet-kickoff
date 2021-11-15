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

    public Task<Response<AddUserRoleResponse>> AddUserRoleAsync(AddUserRoleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<DeleteUserResponse>> DeleteUserAsync(DeleteUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<DeleteUserRoleResponse>> DeleteUserRoleAsync(DeleteUserRoleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<SaveMultipleUserResponse>> SaveMultipleUserAsync(SaveMultipleUserRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<SaveUserResponse>> SaveUserAsync(SaveUserRequest request)
    {
        Response<SaveUserResponse> response = new();
        User user = _mapper.Map<User>(request);

        await _userManager.CreateAsync(user);

        return response;
    }

    public async Task<Response<SearchUsersResponse>> SearchUsersAsync(SearchUsersRequest request)
    {
        Response<SearchUsersResponse> response = new();
        // Todo : Get Users By PaginatedListAsync
        return response;
    }

    public async Task<Response<UpdateUserResponse>> UpdateUserAsync(UpdateUserRequest request)
    {
        Response<UpdateUserResponse> response = new();
        User user = _mapper.Map<User>(request);

        await _userManager.UpdateAsync(user);

        return response;
    }
}