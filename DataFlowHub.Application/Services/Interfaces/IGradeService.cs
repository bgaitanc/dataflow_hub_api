using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;

namespace DataFlowHub.Application.Services.Interfaces;

public interface IGradeService
{
    Task<GradeDto> GetByIdAsync(Guid id);
    Task<PagedResultDto<GradeDto>> GetPagedAsync(GradeFilterDto filter);
    Task<Guid> CreateAsync(CreateGradeDto createDto);
    Task UpdateAsync(Guid id, UpdateGradeDto updateDto);
    Task DeleteAsync(Guid id);
}
