using DataFlowHub.Domain.Entities.Actors;
using DataFlowHub.Domain.Repositories.Interfaces;
using DataFlowHub.Infrastructure.Database;

namespace DataFlowHub.Infrastructure.Repositories.Implementation;

public class StudentRepository(DataFlowHubDbContext context)
    : GenericRepository<Student>(context), IStudentRepository
{
}
