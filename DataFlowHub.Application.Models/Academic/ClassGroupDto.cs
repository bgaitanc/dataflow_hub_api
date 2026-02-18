namespace DataFlowHub.Application.Models.Academic;

public record ClassGroupDto(
    Guid Id,
    string Name,
    string CourseName,
    string TeacherName,
    int EnrolledCount,
    int MaxCapacity,
    string TermName
);