using DataFlowHub.Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFlowHub.Infrastructure.Database.Configuration.Academic;

public class ClassGroupConfiguration : IEntityTypeConfiguration<ClassGroup>
{
    public void Configure(EntityTypeBuilder<ClassGroup> builder)
    {
        builder.ToTable("ClassGroups", "academic");

        builder.HasKey(cg => cg.Id);
        builder.Property(cg => cg.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(cg => cg.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(cg => cg.MaxCapacity)
            .IsRequired();

        builder.Property(cg => cg.CourseId).IsRequired().HasColumnType("uuid");
        builder.Property(cg => cg.TeacherId).IsRequired().HasColumnType("uuid");
        builder.Property(cg => cg.AcademicTermId).IsRequired().HasColumnType("uuid");

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_ClassGroups_Id_NotDefault", $"\"Id\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_ClassGroups_CourseId_NotDefault", $"\"CourseId\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_ClassGroups_TeacherId_NotDefault", $"\"TeacherId\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_ClassGroups_AcademicTermId_NotDefault", $"\"AcademicTermId\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_ClassGroups_MaxCapacity_Range", "\"MaxCapacity\" > 0 AND \"MaxCapacity\" < 60");
        });

        builder.HasOne(cg => cg.Course)
            .WithMany(c => c.ClassGroups)
            .HasForeignKey(cg => cg.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cg => cg.Teacher)
            .WithMany(t => t.ClassGroups)
            .HasForeignKey(cg => cg.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cg => cg.AcademicTerm)
            .WithMany(at => at.ClassGroups)
            .HasForeignKey(cg => cg.AcademicTermId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
