using DataFlowHub.Domain.Entities.Academic;
using DataFlowHub.Domain.Repositories.Interfaces;
using DataFlowHub.Infrastructure.Database;

namespace DataFlowHub.Infrastructure.Repositories.Implementation;

public class AcademicTermRepository(DataFlowHubDbContext context)
    : GenericRepository<AcademicTerm>(context), IAcademicTermRepository
{
}
