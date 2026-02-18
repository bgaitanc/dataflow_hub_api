namespace DataFlowHub.Application.Models.Academic;

public record CreateClassGroupDto(
    string Name,
    int MaxCapacity,
    Guid CourseId,
    Guid TeacherId,
    Guid AcademicTermId
);
