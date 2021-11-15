using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Repomine.Dotnet.Core.Application.DTOs.Email;
using Repomine.Dotnet.Core.Application.DTOs.Identity;
using Repomine.Dotnet.Core.Application.Exceptions;
using Repomine.Dotnet.Core.Application.Interfaces.Identity;
using Repomine.Dotnet.Core.Domain.Wrappers;
using Repomine.Dotnet.Core.Domain.Enums;
using Repomine.Dotnet.Infrastructure.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Repomine.Dotnet.Core.Application.Interfaces.Shared;
using Microsoft.AspNetCore.WebUtilities;
using Repomine.Dotnet.Infrastructure.Identity.Helpers;

namespace Repomine.Dotnet.Infrastructure.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IEmailService _emailService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IEmailService emailService, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Response<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new ApiException($"No Accounts Registered with {request.Email}.");
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            throw new ApiException($"Invalid Credentials for '{request.Email}'.");
        }

        if (!user.EmailConfirmed)
        {
            throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
        }

        JwtSecurityToken jwtSecurityToken = await AuthHelper.GenerateAccessToken(user, _userManager, _jwtSettings);
        var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        var refreshToken = AuthHelper.GenerateRefreshToken();
        LoginResponse response = new()
        {
            Id = user.Id,
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName,
            Roles = rolesList.ToList(),
            IsVerified = user.EmailConfirmed,
            RefreshToken = refreshToken.Token
        };
                
        return new Response<LoginResponse>(response, $"Authenticated {user.UserName}");
    }

    public Task<Response<bool>> LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Response<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<RegisterResponse>> RegisterAsync(RegisterRequest request, string origin)
    {
        RegisterResponse response = new();
        var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
        if (userWithSameUserName != null)
        {
            throw new ApiException($"Username '{request.UserName}' is already taken.");
        }

        var user = new User
        {
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname,
            UserName = request.UserName,
            Gender = request.Gender,
        };

        var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userWithSameEmail == null)
        {
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());
                var confirmUrl = await AuthHelper.GetConfirmUrl(user, origin, _userManager);
                //TODO: Attach Email Service here and configure it via appsettings
                await _emailService.SendEmailAsync(new SendEmailAsyncRequest() { From = "repomine@outlook.com", To = user.Email, Body = $"Please confirm your account by visiting this URL {confirmUrl}", Subject = "Confirm Registration" });
                return new Response<RegisterResponse>(response, message: $"User Registered. Please confirm your account by visiting this URL {confirmUrl}");
            }
            else
            {
                throw new ApiException($"{result.Errors}");
            }
        }
        else
        {
            throw new ApiException($"Email {request.Email } is already registered.");
        }
    }

    public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);
        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded)
        {
            return new Response<string>(user.Id, message: $"Account Confirmed for {user.Email}. You can now use the /auth endpoint.");
        }
        else
        {
            throw new ApiException($"An error occured while confirming {user.Email}.");
        }
    }
}