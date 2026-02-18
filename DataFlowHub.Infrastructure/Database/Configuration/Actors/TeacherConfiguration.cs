using DataFlowHub.Domain.Entities.Actors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFlowHub.Infrastructure.Database.Configuration.Actors;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers", "actors");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(s => s.EmployeeNumber).IsRequired().HasColumnType("varchar(10)").HasMaxLength(10);
        builder.HasIndex(s => s.EmployeeNumber).IsUnique();
        builder.Property(s => s.Specialization).IsRequired(false).HasColumnType("varchar(50)").HasMaxLength(50);
        builder.Property(s => s.Title).IsRequired(false).HasColumnType("varchar(50)").HasMaxLength(50);
        builder.Property(s => s.UserId).HasColumnType("uuid");

        builder.HasOne(t => t.User)
            .WithOne(u => u.TeacherProfile)
            .HasForeignKey<Teacher>(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.ClassGroups).WithOne(e => e.Teacher).HasForeignKey(e => e.TeacherId);
        
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Teachers_Id_NotDefault", $"\"Id\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_Teachers_UserId_NotDefault", $"\"UserId\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_Teachers_EmployeeNumber_NotEmpty", "TRIM(\"EmployeeNumber\") <> ''");
        });
    }
}