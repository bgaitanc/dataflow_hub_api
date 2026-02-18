namespace DataFlowHub.Application.Models.Actors;

public record GradeDto(
    Guid Id,
    string AssessmentName,
    decimal Score,
    decimal WeightPercentage,
    DateTime DateRecorded
);