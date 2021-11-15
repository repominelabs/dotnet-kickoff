using System.Text.Json.Serialization;

namespace Repomine.Dotnet.Core.Application.DTOs.Identity;

public class LoginResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public bool IsVerified { get; set; }
    public string AccessToken { get; set; }
    [JsonIgnore]
    public string RefreshToken { get; set; }
}