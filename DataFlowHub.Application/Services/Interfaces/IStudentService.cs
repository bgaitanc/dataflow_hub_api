using DataFlowHub.Application.Models.Actors;
using DataFlowHub.Application.Models.Common;

namespace DataFlowHub.Application.Services.Interfaces;

public interface IStudentService
{
    Task<StudentDto> GetByIdAsync(Guid id);
    Task<PagedResultDto<StudentDto>> GetPagedAsync(BaseFilterDto filter);
    Task<Guid> CreateAsync(CreateStudentDto createDto);
    Task UpdateAsync(Guid id, CreateStudentDto updateDto);
    Task DeleteAsync(Guid id);
}
