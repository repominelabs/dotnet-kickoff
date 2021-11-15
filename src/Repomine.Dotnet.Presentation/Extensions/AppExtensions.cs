using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repomine.Dotnet.Infrastructure.Identity.Models;
using Repomine.Dotnet.Infrastructure.Identity.Seeds;

namespace Repomine.Dotnet.Presentation.Extensions;

public static class AppExtensions
{
    public static void UseSwaggerExtension(this WebApplication app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Repomine.Dotnet.Presentation v1");
        });
    }

    public static async Task SeedIdentityAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<Repomine.Dotnet.Infrastructure.Identity.Contexts.IdentityDbContext>();

            if (context.Database.IsMySql())
            {
                context.Database.Migrate();
            }

            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<Role>>();

            await DefaultRoles.SeedAsync(userManager, roleManager);
            await DefaultUsers.SeedAsync(userManager, roleManager);
            // Todo: log
        }
        catch (Exception ex)
        {
            // Todo: log
        }
    }
}