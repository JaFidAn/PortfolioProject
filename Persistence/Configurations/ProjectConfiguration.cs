using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        // Primary key
        builder.HasKey(p => p.Id);

        // Properties
        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Description)
               .IsRequired();

        builder.Property(p => p.Link)
               .IsRequired()
               .HasMaxLength(300);

        // Relationships
        builder.HasMany(p => p.ProjectTechnologies)
               .WithOne(pt => pt.Project)
               .HasForeignKey(pt => pt.ProjectId);
    }
}
