namespace DataFlowHub.Application.Models.Academic;

public record AcademicTermDto(
    Guid Id,
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    bool IsCurrent
);

public record CreateAcademicTermDto(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    bool IsCurrent
);

public record UpdateAcademicTermDto(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    bool IsCurrent
);
