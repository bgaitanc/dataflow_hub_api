namespace DataFlowHub.Application.Models.Actors;

public record CreateStudentDto(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string StudentCode,
    DateTime DateOfBirth,
    string? Address
);