using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projects.Domain.Entities.Projects;

namespace Projects.Infrastructure.Persistence.Configurations;

public sealed class ProjectConfiguration
    : IEntityTypeConfiguration<Project>
{
    public void Configure(
        EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}