using DataFlowHub.Domain.Entities.Base;

namespace DataFlowHub.Domain.Entities.Academic;

public class Grade : BaseEntity
{
    public required string AssessmentName { get; set; }
    public decimal Score { get; set; }
    public decimal WeightPercentage { get; set; }
    public Guid EnrollmentId { get; set; }
    public Enrollment Enrollment { get; set; } = null!;
}