using DataFlowHub.Domain.Entities.Academic;
using DataFlowHub.Domain.Entities.Base;
using DataFlowHub.Domain.Entities.Identity;

namespace DataFlowHub.Domain.Entities.Actors;

public class Student : BaseEntity
{
    public required string StudentCode { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Address { get; set; }
    public required Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}