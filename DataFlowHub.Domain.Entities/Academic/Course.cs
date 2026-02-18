using DataFlowHub.Domain.Entities.Base;

namespace DataFlowHub.Domain.Entities.Academic;

public class Course : BaseEntity
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public int Credits { get; set; }
    public string? Description { get; set; }

    public ICollection<ClassGroup> ClassGroups { get; set; } = new List<ClassGroup>();
}