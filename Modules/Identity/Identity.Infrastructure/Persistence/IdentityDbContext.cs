using Identity.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence;

public sealed class IdentityDbContext
    : DbContext
{
    public DbSet<User> Users => Set<User>();

    public IdentityDbContext(
        DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IdentityDbContext).Assembly);

        modelBuilder.Entity<User>()
            .HasQueryFilter(x => !x.IsDeleted);

    }
}
