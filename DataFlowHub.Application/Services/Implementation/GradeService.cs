using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;
using DataFlowHub.Application.Services.Interfaces;
using DataFlowHub.Domain.Entities.Academic;
using DataFlowHub.Domain.Exceptions;
using DataFlowHub.Domain.Repositories.Interfaces;

namespace DataFlowHub.Application.Services.Implementation;

public class GradeService(IGradeRepository gradeRepository) : IGradeService
{
    public async Task<GradeDto> GetByIdAsync(Guid id)
    {
        var (items, _) = await gradeRepository.GetPagedAsync(
            1, 1, g => g.Id == id, null, "Enrollment.Student.User");
        
        var grade = items.FirstOrDefault()
                    ?? throw new NotFoundException(nameof(Grade), id);

        return MapToDto(grade);
    }

    public async Task<PagedResultDto<GradeDto>> GetPagedAsync(GradeFilterDto filter)
    {
        var (items, totalCount) = await gradeRepository.GetPagedAsync(
            filter.PageNumber,
            filter.PageSize,
            g => (!filter.EnrollmentId.HasValue || g.EnrollmentId == filter.EnrollmentId) &&
                 (string.IsNullOrEmpty(filter.AssessmentName) || g.AssessmentName.Contains(filter.AssessmentName)),
            q => q.OrderBy(g => g.AssessmentName),
            "Enrollment.Student.User"
        );

        return new PagedResultDto<GradeDto>
        {
            Items = items.Select(MapToDto),
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<Guid> CreateAsync(CreateGradeDto createDto)
    {
        var grade = new Grade
        {
            EnrollmentId = createDto.EnrollmentId,
            AssessmentName = createDto.AssessmentName,
            Score = createDto.Score,
            WeightPercentage = createDto.WeightPercentage
        };

        await gradeRepository.AddAsync(grade);
        await gradeRepository.SaveChangesAsync();

        return grade.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateGradeDto updateDto)
    {
        var grade = await gradeRepository.GetByIdAsync(id)
                    ?? throw new NotFoundException(nameof(Grade), id);

        grade.AssessmentName = updateDto.AssessmentName;
        grade.Score = updateDto.Score;
        grade.WeightPercentage = updateDto.WeightPercentage;

        gradeRepository.Update(grade);
        await gradeRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var grade = await gradeRepository.GetByIdAsync(id)
                    ?? throw new NotFoundException(nameof(Grade), id);

        gradeRepository.Delete(grade);
        await gradeRepository.SaveChangesAsync();
    }

    private static GradeDto MapToDto(Grade g)
    {
        return new GradeDto(
            g.Id,
            g.EnrollmentId,
            g.Enrollment?.Student?.User != null ? $"{g.Enrollment.Student.User.FirstName} {g.Enrollment.Student.User.LastName}" : "N/A",
            g.AssessmentName,
            g.Score,
            g.WeightPercentage
        );
    }
}
