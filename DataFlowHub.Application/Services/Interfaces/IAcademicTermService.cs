using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;

namespace DataFlowHub.Application.Services.Interfaces;

public interface IAcademicTermService
{
    Task<AcademicTermDto> GetByIdAsync(Guid id);
    Task<PagedResultDto<AcademicTermDto>> GetPagedAsync(AcademicTermFilterDto filter);
    Task<Guid> CreateAsync(CreateAcademicTermDto createDto);
    Task UpdateAsync(Guid id, UpdateAcademicTermDto updateDto);
    Task DeleteAsync(Guid id);
}
