using Microsoft.AspNetCore.Identity;

namespace Repomine.Dotnet.Infrastructure.Identity.Models;

public class Role : IdentityRole
{
    public Role(string roleName) : base(roleName) { }
    public Role() { }
}