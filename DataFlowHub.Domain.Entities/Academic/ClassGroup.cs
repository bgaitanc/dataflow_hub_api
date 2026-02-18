using DataFlowHub.Domain.Entities.Actors;
using DataFlowHub.Domain.Entities.Base;

namespace DataFlowHub.Domain.Entities.Academic;

public class ClassGroup : BaseEntity
{
    public required string Name { get; set; }
    public int MaxCapacity { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public Guid AcademicTermId { get; set; }
    public AcademicTerm AcademicTerm { get; set; } = null!;

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}