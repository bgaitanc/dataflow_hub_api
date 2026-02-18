namespace DataFlowHub.Application.Models.Actors;

public record AddGradeDto(
    Guid EnrollmentId,
    string AssessmentName,
    decimal Score,
    decimal WeightPercentage
);