using Microsoft.EntityFrameworkCore;
using Repomine.Dotnet.Core.Domain.Entities;
using Repomine.Dotnet.Core.Application.Interfaces.Contexts;

namespace Repomine.Dotnet.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*
        modelBuilder.Entity<Group>().HasData(
                new Group { GroupId = 0, GroupName = "Group0", GroupDescription = "GroupDescription0" },
                new Group { GroupId = 1, GroupName = "Group1", GroupDescription = "GroupDescription1" },
                new Group { GroupId = 2, GroupName = "Group2", GroupDescription = "GroupDescription2" }
                );


        modelBuilder.Entity<User>().HasData(
                new User { UserId = 0, Username = "User0", Password = "", Email = "user0@repomine.com" },
                new User { UserId = 1, Username = "User1", Password = "", Email = "user1@repomine.com" },
                new User { UserId = 2, Username = "User2", Password = "", Email = "user2@repomine.com" }
                );
        */
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    // entry.Entity.CreatedBy = _authenticatedUser.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    // entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}