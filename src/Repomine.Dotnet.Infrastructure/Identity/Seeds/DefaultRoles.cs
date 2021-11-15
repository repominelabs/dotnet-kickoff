using Microsoft.AspNetCore.Identity;
using Repomine.Dotnet.Core.Domain.Enums;
using Repomine.Dotnet.Infrastructure.Identity.Models;

namespace Repomine.Dotnet.Infrastructure.Identity.Seeds;

public static class DefaultRoles
{
    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        // Seed Roles
        await roleManager.CreateAsync(new Role(Roles.Admin.ToString()));
        await roleManager.CreateAsync(new Role(Roles.Vendor.ToString()));
        await roleManager.CreateAsync(new Role(Roles.Customer.ToString()));
    }
}