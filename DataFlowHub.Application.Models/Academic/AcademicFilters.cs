using DataFlowHub.Application.Models.Common;
using DataFlowHub.Domain.Entities.Enums;

namespace DataFlowHub.Application.Models.Academic;

public class CourseFilterDto : BaseFilterDto
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class AcademicTermFilterDto : BaseFilterDto
{
    public string? Name { get; set; }
    public bool? IsCurrent { get; set; }
}

public class ClassGroupFilterDto : BaseFilterDto
{
    public string? Name { get; set; }
    public Guid? CourseId { get; set; }
    public Guid? TeacherId { get; set; }
    public Guid? AcademicTermId { get; set; }
}

public class EnrollmentFilterDto : BaseFilterDto
{
    public Guid? StudentId { get; set; }
    public Guid? ClassGroupId { get; set; }
    public EnrollmentStatus? Status { get; set; }
}

public class GradeFilterDto : BaseFilterDto
{
    public Guid? EnrollmentId { get; set; }
    public string? AssessmentName { get; set; }
}
