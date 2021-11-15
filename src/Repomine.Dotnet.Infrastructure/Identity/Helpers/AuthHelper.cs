using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Repomine.Dotnet.Core.Domain.Wrappers;
using Repomine.Dotnet.Infrastructure.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Repomine.Dotnet.Infrastructure.Identity.Helpers;

public class AuthHelper
{
    public static async Task<JwtSecurityToken> GenerateAccessToken(User user, UserManager<User> userManager, JwtSettings jwtSettings)
    {
        // Get Ip Address
        string ipAddress = GetIpAddress();

        // Get & Set Claims
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim("roles", roles[i]));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id),
            new Claim("ip", ipAddress)
        }
        .Union(userClaims)
        .Union(roleClaims);

        // Get  related keys & credentials
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        // Create Access Token
        var accessToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return accessToken;
    }

    public static RefreshToken GenerateRefreshToken()
    {
        // Create Refresh Token & return
        return new RefreshToken
        {
            Token = RandomTokenString(),
            Expires = DateTime.UtcNow.AddHours(4),
            Created = DateTime.UtcNow,
            CreatedByIp = GetIpAddress()
        };
    }

    public static async Task<string> GetConfirmUrl(User user, string origin, UserManager<User> userManager)
    {
        // Get rest endpoint url
        var route = "auth/confirm-email/";
        var endpointUrl = new Uri(string.Concat($"{origin}/", route));

        // Generate Email Confirmation Token
        var confirmToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
        confirmToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmToken));

        // Create full Confirm Url
        var confirmUrl = QueryHelpers.AddQueryString(endpointUrl.ToString(), "userId", user.Id);
        confirmUrl = QueryHelpers.AddQueryString(confirmUrl, "code", confirmToken);

        return confirmUrl;
    }

    private static string GetIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        return string.Empty;
    }

    private static string RandomTokenString()
    {
        var randomBytes = new byte[40];

        // Define RNGCrypto Service Provider
        using RNGCryptoServiceProvider rngCryptoServiceProvider = new();
        rngCryptoServiceProvider.GetBytes(randomBytes);

        // Convert random bytes to hex string & return
        return BitConverter.ToString(randomBytes).Replace("-", "");
    }
}