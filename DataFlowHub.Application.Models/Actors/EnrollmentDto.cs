using DataFlowHub.Domain.Entities.Enums;

namespace DataFlowHub.Application.Models.Actors;

public record EnrollmentDto(
    Guid Id,
    Guid StudentId,
    string StudentName,
    Guid ClassGroupId,
    string GroupName,
    string CourseName,
    DateTime EnrollmentDate,
    EnrollmentStatus Status,
    decimal CurrentAverage
);