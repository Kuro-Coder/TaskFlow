using Microsoft.EntityFrameworkCore;
using Projects.Domain.Entities;

namespace Projects.Infrastructure.Persistence;

public sealed class ProjectsDbContext
    : DbContext
{
    public DbSet<Project> Projects => Set<Project>();

    public ProjectsDbContext(
        DbContextOptions<ProjectsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ProjectsDbContext).Assembly);
    }
}