using Repomine.Dotnet.Core.Application.DTOs.Identity.User;
using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Core.Application.Interfaces.Identity;

public interface IUserService
{
    Task<Response<SearchUsersResponse>> SearchUsersAsync(SearchUsersRequest request);
    Task<Response<string>> SaveUserAsync(SaveUserRequest request);
    Task<Response<string>> SaveMultipleUserAsync(SaveMultipleUserRequest request);
    Task<Response<string>> DeleteUserAsync(DeleteUserRequest request);
    Task<Response<string>> UpdateUserAsync(UpdateUserRequest request);
    Task<Response<string>> AddUserRoleAsync(AddUserRoleRequest request);
    Task<Response<string>> DeleteUserRoleAsync(DeleteUserRoleRequest request);
}