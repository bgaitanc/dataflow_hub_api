using DataFlowHub.Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFlowHub.Infrastructure.Database.Configuration.Academic;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments", "academic");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(e => e.EnrollmentDate).IsRequired();
        
        builder.Property(e => e.Status)
            .HasColumnType("enrollment_status")
            .HasDefaultValueSql("'active'::enrollment_status"); 
        
        builder.Property(e => e.StudentId).IsRequired().HasColumnType("uuid");
        builder.Property(e => e.ClassGroupId).IsRequired().HasColumnType("uuid");
        builder.HasIndex(e => new { e.StudentId, e.ClassGroupId }).IsUnique();
        
        builder.HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(e => e.ClassGroup)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.ClassGroupId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Enrollments_Id_NotDefault", $"\"Id\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_Enrollments_StudentId_NotDefault", $"\"StudentId\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_Enrollments_ClassGroupId_NotDefault", $"\"ClassGroupId\" <> '{Guid.Empty}'");
        });
    }
}