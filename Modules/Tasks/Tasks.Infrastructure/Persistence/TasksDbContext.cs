using Microsoft.EntityFrameworkCore;

namespace Tasks.Infrastructure.Persistence;

public sealed class TasksDbContext : DbContext
{

    public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(TasksDbContext).Assembly);


    }
}