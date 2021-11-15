using Microsoft.EntityFrameworkCore;
using Repomine.Dotnet.Core.Domain.Entities;

namespace Repomine.Dotnet.Core.Application.Interfaces.Contexts;

public interface IApplicationDbContext
{
    DbSet<Order> Orders { get; set; }
    DbSet<Product> Products { get; set; }
}
