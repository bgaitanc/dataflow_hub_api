namespace DataFlowHub.Application.Models.Actors;

public record TeacherDto(
    Guid Id,
    string FullName,
    string Email,
    string EmployeeNumber,
    string? Specialization
);