using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Repomine.Dotnet.Core;

public static class ServiceRegistration
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}