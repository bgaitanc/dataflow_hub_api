using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;
using DataFlowHub.Application.Services.Interfaces;
using DataFlowHub.Domain.Entities.Academic;
using DataFlowHub.Domain.Exceptions;
using DataFlowHub.Domain.Repositories.Interfaces;

namespace DataFlowHub.Application.Services.Implementation;

public class AcademicTermService(IAcademicTermRepository academicTermRepository) : IAcademicTermService
{
    public async Task<AcademicTermDto> GetByIdAsync(Guid id)
    {
        var term = await academicTermRepository.GetByIdAsync(id)
                   ?? throw new NotFoundException(nameof(AcademicTerm), id);

        return new AcademicTermDto(
            term.Id,
            term.Name,
            term.StartDate,
            term.EndDate,
            term.IsCurrent
        );
    }

    public async Task<PagedResultDto<AcademicTermDto>> GetPagedAsync(AcademicTermFilterDto filter)
    {
        var (items, totalCount) = await academicTermRepository.GetPagedAsync(
            filter.PageNumber,
            filter.PageSize,
            t => (string.IsNullOrEmpty(filter.Name) || t.Name.Contains(filter.Name)) &&
                 (!filter.IsCurrent.HasValue || t.IsCurrent == filter.IsCurrent),
            q => q.OrderByDescending(t => t.StartDate)
        );

        return new PagedResultDto<AcademicTermDto>
        {
            Items = items.Select(t => new AcademicTermDto(
                t.Id,
                t.Name,
                t.StartDate,
                t.EndDate,
                t.IsCurrent
            )),
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<Guid> CreateAsync(CreateAcademicTermDto createDto)
    {
        var term = new AcademicTerm
        {
            Name = createDto.Name,
            StartDate = createDto.StartDate,
            EndDate = createDto.EndDate,
            IsCurrent = createDto.IsCurrent
        };

        await academicTermRepository.AddAsync(term);
        await academicTermRepository.SaveChangesAsync();

        return term.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateAcademicTermDto updateDto)
    {
        var term = await academicTermRepository.GetByIdAsync(id)
                   ?? throw new NotFoundException(nameof(AcademicTerm), id);

        term.Name = updateDto.Name;
        term.StartDate = updateDto.StartDate;
        term.EndDate = updateDto.EndDate;
        term.IsCurrent = updateDto.IsCurrent;

        academicTermRepository.Update(term);
        await academicTermRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var term = await academicTermRepository.GetByIdAsync(id)
                   ?? throw new NotFoundException(nameof(AcademicTerm), id);

        academicTermRepository.Delete(term);
        await academicTermRepository.SaveChangesAsync();
    }
}
