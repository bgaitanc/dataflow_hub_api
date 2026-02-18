namespace DataFlowHub.Application.Models.Actors;

public record EnrollStudentDto(
    Guid StudentId,
    Guid ClassGroupId
);