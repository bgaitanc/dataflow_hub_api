using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;
using DataFlowHub.Application.Services.Interfaces;
using DataFlowHub.Domain.Entities.Academic;
using DataFlowHub.Domain.Exceptions;
using DataFlowHub.Domain.Repositories.Interfaces;

namespace DataFlowHub.Application.Services.Implementation;

public class EnrollmentService(IEnrollmentRepository enrollmentRepository) : IEnrollmentService
{
    public async Task<EnrollmentDto> GetByIdAsync(Guid id)
    {
        var (items, _) = await enrollmentRepository.GetPagedAsync(
            1, 1, e => e.Id == id, null, "Student.User,ClassGroup");
        
        var enrollment = items.FirstOrDefault()
                         ?? throw new NotFoundException(nameof(Enrollment), id);

        return MapToDto(enrollment);
    }

    public async Task<PagedResultDto<EnrollmentDto>> GetPagedAsync(EnrollmentFilterDto filter)
    {
        var (items, totalCount) = await enrollmentRepository.GetPagedAsync(
            filter.PageNumber,
            filter.PageSize,
            e => (!filter.StudentId.HasValue || e.StudentId == filter.StudentId) &&
                 (!filter.ClassGroupId.HasValue || e.ClassGroupId == filter.ClassGroupId) &&
                 (!filter.Status.HasValue || e.Status == filter.Status),
            q => q.OrderByDescending(e => e.EnrollmentDate),
            "Student.User,ClassGroup"
        );

        return new PagedResultDto<EnrollmentDto>
        {
            Items = items.Select(MapToDto),
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<Guid> CreateAsync(CreateEnrollmentDto createDto)
    {
        var (existing, _) = await enrollmentRepository.GetPagedAsync(1, 1, 
            e => e.StudentId == createDto.StudentId && e.ClassGroupId == createDto.ClassGroupId);
        
        if (existing.Any())
            throw new DomainException("El estudiante ya está inscrito en este grupo.");

        var enrollment = new Enrollment
        {
            StudentId = createDto.StudentId,
            ClassGroupId = createDto.ClassGroupId,
            EnrollmentDate = DateTime.Now,
            Status = Domain.Entities.Enums.EnrollmentStatus.Active
        };

        await enrollmentRepository.AddAsync(enrollment);
        await enrollmentRepository.SaveChangesAsync();

        return enrollment.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateEnrollmentDto updateDto)
    {
        var enrollment = await enrollmentRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException(nameof(Enrollment), id);

        enrollment.Status = updateDto.Status;

        enrollmentRepository.Update(enrollment);
        await enrollmentRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var enrollment = await enrollmentRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException(nameof(Enrollment), id);

        enrollmentRepository.Delete(enrollment);
        await enrollmentRepository.SaveChangesAsync();
    }

    private static EnrollmentDto MapToDto(Enrollment e)
    {
        return new EnrollmentDto(
            e.Id,
            e.StudentId,
            e.Student?.User != null ? $"{e.Student.User.FirstName} {e.Student.User.LastName}" : "N/A",
            e.ClassGroupId,
            e.ClassGroup?.Name ?? "N/A",
            e.EnrollmentDate,
            e.Status
        );
    }
}
