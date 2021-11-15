using Microsoft.AspNetCore.Mvc;
using Repomine.Dotnet.Core.Application.Interfaces.Identity;

namespace Repomine.Dotnet.Presentation.Controllers.Identity;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}