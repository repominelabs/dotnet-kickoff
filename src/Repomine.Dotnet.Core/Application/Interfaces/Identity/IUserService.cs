using Repomine.Dotnet.Core.Application.DTOs.Identity.User;
using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Core.Application.Interfaces.Identity;

public interface IUserService
{
    Task<Response<SearchUsersResponse>> SearchUsers(SearchUsersRequest request);
    Task<Response<SaveUserResponse>> SaveUser(SaveUserRequest request);
    Task<Response<SaveMultipleUserResponse>> SaveMultipleUser(SaveMultipleUserRequest request);
    Task<Response<DeleteUserResponse>> DeleteUser(DeleteUserRequest request);
    Task<Response<UpdateUserResponse>> UpdateUser(UpdateUserRequest request);
    Task<Response<AddUserRoleResponse>> AddUserRole(AddUserRoleRequest request);
    Task<Response<DeleteUserRoleResponse>> DeleteUserRole(DeleteUserRoleRequest request);
}