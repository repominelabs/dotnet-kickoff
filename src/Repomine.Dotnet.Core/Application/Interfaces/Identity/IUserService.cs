using Repomine.Dotnet.Core.Application.DTOs.Identity.User;
using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Core.Application.Interfaces.Identity;

public interface IUserService
{
    Task<Response<SearchUsersResponse>> SearchUsersAsync(SearchUsersRequest request);
    Task<Response<SaveUserResponse>> SaveUserAsync(SaveUserRequest request);
    Task<Response<SaveMultipleUserResponse>> SaveMultipleUserAsync(SaveMultipleUserRequest request);
    Task<Response<DeleteUserResponse>> DeleteUserAsync(DeleteUserRequest request);
    Task<Response<UpdateUserResponse>> UpdateUserAsync(UpdateUserRequest request);
    Task<Response<AddUserRoleResponse>> AddUserRoleAsync(AddUserRoleRequest request);
    Task<Response<DeleteUserRoleResponse>> DeleteUserRoleAsync(DeleteUserRoleRequest request);
}