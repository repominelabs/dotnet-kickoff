using System.ComponentModel.DataAnnotations;

namespace Repomine.Dotnet.Core.Application.DTOs.Identity;

public class RegisterRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    public string UserName { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Gender { get; set; }
}