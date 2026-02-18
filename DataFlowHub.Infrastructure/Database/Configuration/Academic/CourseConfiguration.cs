using DataFlowHub.Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFlowHub.Infrastructure.Database.Configuration.Academic;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses", "academic");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(c => c.Code).IsUnique();

        builder.Property(c => c.Credits)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Courses_Id_NotDefault", $"\"Id\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_Courses_Credits_Range", "\"Credits\" >= 0 AND \"Credits\" <= 3");
        });
    }
}
