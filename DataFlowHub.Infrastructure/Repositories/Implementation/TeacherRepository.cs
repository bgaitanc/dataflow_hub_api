using DataFlowHub.Domain.Entities.Actors;
using DataFlowHub.Domain.Repositories.Interfaces;
using DataFlowHub.Infrastructure.Database;

namespace DataFlowHub.Infrastructure.Repositories.Implementation;

public class TeacherRepository(DataFlowHubDbContext context)
    : GenericRepository<Teacher>(context), ITeacherRepository
{
}
