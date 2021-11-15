using Repomine.Dotnet.Core.Application.DTOs.Email;

namespace Repomine.Dotnet.Core.Application.Interfaces.Shared;

public interface IEmailService
{
    Task SendEmailAsync(SendEmailAsyncRequest request);
}