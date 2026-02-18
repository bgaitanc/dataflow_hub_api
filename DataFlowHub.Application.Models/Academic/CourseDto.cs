namespace DataFlowHub.Application.Models.Academic;

public record CourseDto(
    Guid Id,
    string Name,
    string Code,
    int Credits,
    string? Description,
    bool IsActive
);