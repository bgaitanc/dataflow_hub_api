using DataFlowHub.Domain.Entities.Base;

namespace DataFlowHub.Domain.Entities.Academic;

public class AcademicTerm : BaseEntity
{
    public required string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public ICollection<ClassGroup> ClassGroups { get; set; } = new List<ClassGroup>();
}