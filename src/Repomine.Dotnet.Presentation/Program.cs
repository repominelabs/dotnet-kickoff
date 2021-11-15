using Repomine.Dotnet.Core;
using Repomine.Dotnet.Infrastructure;
using Repomine.Dotnet.Presentation.Extensions;
using Repomine.Dotnet.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddApiVersioningService();
builder.Services.AddSwaggerService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExtension();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();
app.SeedIdentityAsync();

app.Run();