using DataFlowHub.Domain.Entities.Academic;
using DataFlowHub.Domain.Repositories.Interfaces;
using DataFlowHub.Infrastructure.Database;

namespace DataFlowHub.Infrastructure.Repositories.Implementation;

public class EnrollmentRepository(DataFlowHubDbContext context)
    : GenericRepository<Enrollment>(context), IEnrollmentRepository
{
}
