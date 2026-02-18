using DataFlowHub.Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataFlowHub.Infrastructure.Database.Configuration.Academic;

public class AcademicTermConfiguration : IEntityTypeConfiguration<AcademicTerm>
{
    public void Configure(EntityTypeBuilder<AcademicTerm> builder)
    {
        builder.ToTable("AcademicTerms", "academic");

        builder.HasKey(at => at.Id);
        builder.Property(at => at.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(at => at.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(at => at.StartDate)
            .IsRequired();

        builder.Property(at => at.EndDate)
            .IsRequired();

        builder.Property(at => at.IsCurrent)
            .IsRequired()
            .HasDefaultValue(false);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_AcademicTerms_Id_NotDefault", $"\"Id\" <> '{Guid.Empty}'");
            t.HasCheckConstraint("CK_AcademicTerms_Dates_EndAfterStart", "\"EndDate\" > \"StartDate\"");
        });
    }
}
