using Microsoft.AspNetCore.Identity;
using Repomine.Dotnet.Core.Domain.Enums;
using Repomine.Dotnet.Infrastructure.Identity.Models;

namespace Repomine.Dotnet.Infrastructure.Identity.Seeds;

public static class DefaultUsers
{
    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        // Seed Users
        var defaultUser = new User
        {
            UserName = "dave.mitchell",
            Email = "dave.mitchell@outlook.com",
            Name = "Dave",
            Surname = "Mitchell",
            Gender = "male",
            DateOfBirth = DateTime.Now,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "19Passxxx!");
                await userManager.AddToRoleAsync(defaultUser, Roles.Customer.ToString());
            }
        }
    }
}