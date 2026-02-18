namespace DataFlowHub.Application.Models.Actors;

public record CreateTeacherDto(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string EmployeeNumber,
    string? Specialization,
    string? Title
);