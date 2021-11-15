namespace Repomine.Dotnet.Core.Application.DTOs.Identity.Auth;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}