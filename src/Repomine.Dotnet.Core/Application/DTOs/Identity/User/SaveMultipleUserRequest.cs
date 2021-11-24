namespace Repomine.Dotnet.Core.Application.DTOs.Identity.User;

public class SaveMultipleUserRequest
{
    public List<SaveUserRequest> Users { get; set; }
}