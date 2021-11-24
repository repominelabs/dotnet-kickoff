using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Repomine.Dotnet.Core.Application.Interfaces.Identity;
using Repomine.Dotnet.Core.Application.Interfaces.Shared;
using Repomine.Dotnet.Core.Domain.Wrappers;
using Repomine.Dotnet.Infrastructure.Identity.Mappings;
using Repomine.Dotnet.Infrastructure.Identity.Models;
using Repomine.Dotnet.Infrastructure.Identity.Services;
using Repomine.Dotnet.Infrastructure.Persistence.Contexts;
using Repomine.Dotnet.Infrastructure.Shared.Services;
using System.Text;

namespace Repomine.Dotnet.Infrastructure;

public static class ServiceRegistration
{
    public static async void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Identity
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddAutoMapper(typeof(GeneralProfile));

        string connString = configuration.GetConnectionString("IdentityConnection");
        services.AddDbContextPool<Identity.Contexts.IdentityDbContext>(options => options.UseMySql(connString, ServerVersion.AutoDetect(connString), b => b.SchemaBehavior(MySqlSchemaBehavior.Ignore)));
        services.AddIdentity<User, Role>().AddEntityFrameworkStores<Identity.Contexts.IdentityDbContext>().AddDefaultTokenProviders();
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JWTSettings:Issuer"],
                ValidAudience = configuration["JWTSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
            };
            o.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.StatusCode = 500;
                    c.Response.ContentType = "text/plain";
                    return c.Response.WriteAsync(c.Exception.ToString());
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                    return context.Response.WriteAsync(result);
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                    return context.Response.WriteAsync(result);
                },
            };
        });
        #endregion

        #region Persistence
        services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySql(configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(connString), b => b.SchemaBehavior(MySqlSchemaBehavior.Ignore)));
        #endregion

        #region Shared
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<EmailSettings>(configuration.GetSection("SftpSettings"));
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<ISftpService, SftpService>();
        #endregion
    }
}