using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;

namespace DataFlowHub.Application.Services.Interfaces;

public interface IClassGroupService
{
    Task<ClassGroupDto> GetByIdAsync(Guid id);
    Task<PagedResultDto<ClassGroupDto>> GetPagedAsync(ClassGroupFilterDto filter);
    Task<Guid> CreateAsync(CreateClassGroupDto createDto);
    Task UpdateAsync(Guid id, CreateClassGroupDto updateDto);
    Task DeleteAsync(Guid id);
}
