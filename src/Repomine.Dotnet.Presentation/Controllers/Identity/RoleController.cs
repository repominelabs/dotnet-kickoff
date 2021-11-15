using Microsoft.AspNetCore.Mvc;
using Repomine.Dotnet.Core.Application.Interfaces.Identity;

namespace Repomine.Dotnet.Presentation.Controllers.Identity;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}