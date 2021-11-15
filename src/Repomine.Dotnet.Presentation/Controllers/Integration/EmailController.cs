using Microsoft.AspNetCore.Mvc;
using Repomine.Dotnet.Core.Application.DTOs.Email;
using Repomine.Dotnet.Core.Application.Interfaces.Shared;

namespace Repomine.Dotnet.Presentation.Controllers.Integration;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;
    
    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmailAsync([FromBody] SendEmailAsyncRequest request)
    {
        await _emailService.SendEmailAsync(request);
        return Ok();
    }
}