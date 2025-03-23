using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ProjectTechnologyConfiguration : IEntityTypeConfiguration<ProjectTechnology>
{
    public void Configure(EntityTypeBuilder<ProjectTechnology> builder)
    {
        // Composite primary key
        builder.HasKey(pt => new { pt.ProjectId, pt.TechnologyId });

        // Relationships 
        builder.HasOne(pt => pt.Project)
               .WithMany(p => p.ProjectTechnologies)
               .HasForeignKey(pt => pt.ProjectId);

        builder.HasOne(pt => pt.Technology)
               .WithMany(t => t.ProjectTechnologies)
               .HasForeignKey(pt => pt.TechnologyId);
    }
}
