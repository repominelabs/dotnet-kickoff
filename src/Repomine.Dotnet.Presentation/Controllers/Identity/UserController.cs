using Microsoft.AspNetCore.Mvc;
using Repomine.Dotnet.Core.Application.DTOs.Identity.User;
using Repomine.Dotnet.Core.Application.Interfaces.Identity;
using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Presentation.Controllers.Identity;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("search-users")]
    public async Task<IActionResult> SearchUsersAsync([FromBody] SearchUsersRequest request)
    {
        Response<SearchUsersResponse> response = await _userService.SearchUsersAsync(request);
        return Ok(response);
    }

    [HttpPost("save-user")]
    public async Task<IActionResult> SaveUserAsync([FromBody] SaveUserRequest request)
    {
        Response<SaveUserResponse> response = await _userService.SaveUserAsync(request);
        return Ok(response);
    }

    [HttpPost("save-multiple-user")]
    public async Task<IActionResult> SaveMultipleUserAsync([FromBody] SaveMultipleUserRequest request)
    {
        Response<SaveMultipleUserResponse> response = await _userService.SaveMultipleUserAsync(request);
        return Ok(response);
    }

    [HttpPost("update-user")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request)
    {
        Response<UpdateUserResponse> response = await _userService.UpdateUserAsync(request);
        return Ok(response);
    }

    [HttpPost("delete-user")]
    public async Task<IActionResult> DeleteUserAsync(DeleteUserRequest request)
    {
        Response<DeleteUserResponse> response = await _userService.DeleteUserAsync(request);
        return Ok(response);
    }

    [HttpPost("add-user-role")]
    public async Task<IActionResult> AddUserRoleAsync([FromBody] AddUserRoleRequest request)
    {
        Response<AddUserRoleResponse> response = await _userService.AddUserRoleAsync(request);
        return Ok(response);
    }

    [HttpPost("delete-user-role")]
    public async Task<IActionResult> DeleteUserRoleAsync([FromBody] DeleteUserRoleRequest request)
    {
        Response<DeleteUserRoleResponse> response = await _userService.DeleteUserRoleAsync(request);
        return Ok(response);
    }
}