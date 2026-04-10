using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;

namespace DataFlowHub.Application.Services.Interfaces;

public interface ICourseService
{
    Task<CourseDto> GetByIdAsync(Guid id);
    Task<PagedResultDto<CourseDto>> GetPagedAsync(CourseFilterDto filter);
    Task<Guid> CreateAsync(CreateCourseDto createDto);
    Task UpdateAsync(Guid id, CreateCourseDto updateDto);
    Task DeleteAsync(Guid id);
}
