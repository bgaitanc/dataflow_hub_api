using DataFlowHub.Application.Models.Actors;
using DataFlowHub.Application.Models.Common;

namespace DataFlowHub.Application.Services.Interfaces;

public interface ITeacherService
{
    Task<TeacherDto> GetByIdAsync(Guid id);
    Task<PagedResultDto<TeacherDto>> GetPagedAsync(BaseFilterDto filter);
    Task<Guid> CreateAsync(CreateTeacherDto createDto);
    Task UpdateAsync(Guid id, CreateTeacherDto updateDto);
    Task DeleteAsync(Guid id);
}
