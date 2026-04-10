namespace DataFlowHub.Application.Models.Academic;

public record GradeDto(
    Guid Id,
    Guid EnrollmentId,
    string StudentName,
    string AssessmentName,
    decimal Score,
    decimal WeightPercentage
);

public record CreateGradeDto(
    Guid EnrollmentId,
    string AssessmentName,
    decimal Score,
    decimal WeightPercentage
);

public record UpdateGradeDto(
    string AssessmentName,
    decimal Score,
    decimal WeightPercentage
);
