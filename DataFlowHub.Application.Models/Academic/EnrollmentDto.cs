using DataFlowHub.Domain.Entities.Enums;

namespace DataFlowHub.Application.Models.Academic;

public record EnrollmentDto(
    Guid Id,
    Guid StudentId,
    string StudentName,
    Guid ClassGroupId,
    string ClassGroupName,
    DateTime EnrollmentDate,
    EnrollmentStatus Status
);

public record CreateEnrollmentDto(
    Guid StudentId,
    Guid ClassGroupId
);

public record UpdateEnrollmentDto(
    EnrollmentStatus Status
);
