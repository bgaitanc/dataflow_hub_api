using DataFlowHub.Domain.Entities.Actors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFlowHub.Infrastructure.Database.Configuration.Actors;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students", "actors");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(s => s.StudentCode).IsRequired().HasColumnType("varchar(10)").HasMaxLength(10);
        builder.HasIndex(s => s.StudentCode).IsUnique();
        builder.Property(s => s.Address).IsRequired(false).HasColumnType("varchar(255)").HasMaxLength(255);
        builder.Property(s => s.UserId).HasColumnType("uuid");
        
        builder
            .HasOne(s => s.User)
            .WithOne(u => u.StudentProfile)
            .HasForeignKey<Student>(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Enrollments).WithOne(e => e.Student).HasForeignKey(e => e.StudentId);
        
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Students_Id_NotDefault", $"\"Id\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_Students_UserId_NotDefault", $"\"UserId\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_Students_StudentCode_NotEmpty", "TRIM(\"StudentCode\") <> ''");
        });
    }
}