namespace Repomine.Dotnet.Core.Application.DTOs.Email;

public class SendEmailAsyncRequest
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string From { get; set; }
}