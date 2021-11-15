using Repomine.Dotnet.Core.Application.DTOs.Identity;
using Repomine.Dotnet.Core.Domain.Wrappers;

namespace Repomine.Dotnet.Core.Application.Interfaces.Identity;

public interface IAuthService
{
    Task<Response<LoginResponse>> LoginAsync(LoginRequest request);
    Task<Response<Boolean>> LogoutAsync();
    Task<Response<RegisterResponse>> RegisterAsync(RegisterRequest request, string origin);
    Task<Response<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<Response<string>> ConfirmEmailAsync(string userId, string code);
}