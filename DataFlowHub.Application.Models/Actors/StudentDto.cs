namespace DataFlowHub.Application.Models.Actors;

public record StudentDto(
    Guid Id,
    Guid UserId,
    string FullName,
    string Email,
    string StudentCode,
    DateTime DateOfBirth
);