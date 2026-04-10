using DataFlowHub.Domain.Entities.Academic;
using DataFlowHub.Domain.Repositories.Interfaces;
using DataFlowHub.Infrastructure.Database;

namespace DataFlowHub.Infrastructure.Repositories.Implementation;

public class ClassGroupRepository(DataFlowHubDbContext context)
    : GenericRepository<ClassGroup>(context), IClassGroupRepository
{
}
