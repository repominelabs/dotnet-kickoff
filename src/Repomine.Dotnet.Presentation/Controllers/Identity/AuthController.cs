using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repomine.Dotnet.Core.Application.DTOs.Identity;
using Repomine.Dotnet.Core.Application.Interfaces.Identity;
using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Presentation.Controllers.Identity;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        Response<LoginResponse> response = await _authService.LoginAsync(request);
    
        return Ok(response);
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        var origin = Request.Headers["origin"];
        return Ok(await _authService.RegisterAsync(request, origin));
    }

    [HttpPost("refresh-token")]
    public IActionResult RefreshToken()
    {
        return Ok();
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
    {
        return Ok(await _authService.ConfirmEmailAsync(userId, code));
    }

    [HttpGet("confirm-phone")]
    public async Task<IActionResult> ConfirmPhoneAsync([FromQuery] string userId, [FromQuery] string code)
    {
        return Ok(await _authService.ConfirmEmailAsync(userId, code)); // Todo: ConfirmPhoneAsync
    }
}