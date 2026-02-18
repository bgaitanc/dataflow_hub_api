using DataFlowHub.Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFlowHub.Infrastructure.Database.Configuration.Academic;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.ToTable("Grades", "academic");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(g => g.AssessmentName).IsRequired().HasColumnType("varchar(50)").HasMaxLength(50);
        builder.HasIndex(g => new {g.AssessmentName, g.EnrollmentId}).IsUnique();
       
        builder.Property(g => g.Score)
                .HasColumnType("decimal(5,2)");
            
        builder.Property(g => g.WeightPercentage)
                .HasColumnType("decimal(5,2)");
        
        builder.Property(g => g.EnrollmentId).IsRequired().HasColumnType("uuid");
        
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Grades_Id_NotDefault", $"\"Id\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_Grades_EnrollmentId_NotDefault", $"\"EnrollmentId\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_Grades_Score_Range", "\"Score\" >= 0 AND \"Score\" <= \"WeightPercentage\"");
            t.HasCheckConstraint("CK_Grades_WeightPercentage_Range", "\"WeightPercentage\" >= 0 AND \"WeightPercentage\" <= 100");
        });

        builder.HasOne(g => g.Enrollment)
            .WithMany(e => e.Grades)
            .HasForeignKey(g => g.EnrollmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}