using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        // Primary key
        builder.HasKey(t => t.Id);

        // Properties
        builder.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(100);

        // Relationships
        builder.HasMany(t => t.ProjectTechnologies)
               .WithOne(pt => pt.Technology)
               .HasForeignKey(pt => pt.TechnologyId);
    }
}
