using Microsoft.AspNetCore.Identity;

namespace Repomine.Dotnet.Infrastructure.Identity.Models;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<Role> Roles { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
    public bool OwnsToken(string token)
    {
        return this.RefreshTokens?.Find(x => x.Token == token) != null;
    }
}