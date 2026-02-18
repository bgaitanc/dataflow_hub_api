namespace DataFlowHub.Application.Models.Academic;

public record CreateCourseDto(
    string Name,
    string Code,
    int Credits,
    string? Description
);