using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;

namespace DataFlowHub.Application.Services.Interfaces;

public interface IEnrollmentService
{
    Task<EnrollmentDto> GetByIdAsync(Guid id);
    Task<PagedResultDto<EnrollmentDto>> GetPagedAsync(EnrollmentFilterDto filter);
    Task<Guid> CreateAsync(CreateEnrollmentDto createDto);
    Task UpdateAsync(Guid id, UpdateEnrollmentDto updateDto);
    Task DeleteAsync(Guid id);
}
