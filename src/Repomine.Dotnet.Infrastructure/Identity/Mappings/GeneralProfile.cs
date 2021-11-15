using AutoMapper;
using Repomine.Dotnet.Core.Application.DTOs.Identity.User;
using Repomine.Dotnet.Infrastructure.Identity.Models;

namespace Repomine.Dotnet.Infrastructure.Identity.Mappings;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        CreateMap<SaveUserRequest, User>();
        CreateMap<UpdateUserRequest, User>();
    }
}