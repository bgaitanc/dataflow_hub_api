using DataFlowHub.Domain.Entities.Enums;

namespace DataFlowHub.Application.Models.Actors;

public record UpdateEnrollmentStatusDto(
    Guid EnrollmentId,
    EnrollmentStatus NewStatus
);