using DataFlowHub.Domain.Entities.Actors;
using DataFlowHub.Domain.Entities.Base;
using DataFlowHub.Domain.Entities.Enums;

namespace DataFlowHub.Domain.Entities.Academic;

public class Enrollment : BaseEntity
{
    public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public Guid ClassGroupId { get; set; }
    public ClassGroup ClassGroup { get; set; } = null!;
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}