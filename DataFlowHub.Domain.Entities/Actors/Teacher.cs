using DataFlowHub.Domain.Entities.Academic;
using DataFlowHub.Domain.Entities.Base;
using DataFlowHub.Domain.Entities.Identity;

namespace DataFlowHub.Domain.Entities.Actors;

public class Teacher : BaseEntity
{
    public required string EmployeeNumber { get; set; }
    public string? Specialization { get; set; }
    public string? Title { get; set; }
    public required Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public ICollection<ClassGroup> ClassGroups { get; set; } = new List<ClassGroup>();
}