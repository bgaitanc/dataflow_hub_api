using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;
using DataFlowHub.Application.Services.Interfaces;
using DataFlowHub.Domain.Entities.Academic;
using DataFlowHub.Domain.Exceptions;
using DataFlowHub.Domain.Repositories.Interfaces;

namespace DataFlowHub.Application.Services.Implementation;

public class ClassGroupService(IClassGroupRepository classGroupRepository) : IClassGroupService
{
    public async Task<ClassGroupDto> GetByIdAsync(Guid id)
    {
        var (items, _) = await classGroupRepository.GetPagedAsync(
            1, 1, cg => cg.Id == id, null, "Course,Teacher.User,AcademicTerm,Enrollments");
        
        var classGroup = items.FirstOrDefault()
                         ?? throw new NotFoundException(nameof(ClassGroup), id);

        return MapToDto(classGroup);
    }

    public async Task<PagedResultDto<ClassGroupDto>> GetPagedAsync(ClassGroupFilterDto filter)
    {
        var (items, totalCount) = await classGroupRepository.GetPagedAsync(
            filter.PageNumber,
            filter.PageSize,
            cg => (string.IsNullOrEmpty(filter.Name) || cg.Name.Contains(filter.Name)) &&
                 (!filter.CourseId.HasValue || cg.CourseId == filter.CourseId) &&
                 (!filter.TeacherId.HasValue || cg.TeacherId == filter.TeacherId) &&
                 (!filter.AcademicTermId.HasValue || cg.AcademicTermId == filter.AcademicTermId),
            q => q.OrderBy(cg => cg.Name),
            "Course,Teacher.User,AcademicTerm,Enrollments"
        );

        return new PagedResultDto<ClassGroupDto>
        {
            Items = items.Select(MapToDto),
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<Guid> CreateAsync(CreateClassGroupDto createDto)
    {
        var classGroup = new ClassGroup
        {
            Name = createDto.Name,
            MaxCapacity = createDto.MaxCapacity,
            CourseId = createDto.CourseId,
            TeacherId = createDto.TeacherId,
            AcademicTermId = createDto.AcademicTermId
        };

        await classGroupRepository.AddAsync(classGroup);
        await classGroupRepository.SaveChangesAsync();

        return classGroup.Id;
    }

    public async Task UpdateAsync(Guid id, CreateClassGroupDto updateDto)
    {
        var classGroup = await classGroupRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException(nameof(ClassGroup), id);

        classGroup.Name = updateDto.Name;
        classGroup.MaxCapacity = updateDto.MaxCapacity;
        classGroup.CourseId = updateDto.CourseId;
        classGroup.TeacherId = updateDto.TeacherId;
        classGroup.AcademicTermId = updateDto.AcademicTermId;

        classGroupRepository.Update(classGroup);
        await classGroupRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var classGroup = await classGroupRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException(nameof(ClassGroup), id);

        classGroupRepository.Delete(classGroup);
        await classGroupRepository.SaveChangesAsync();
    }

    private static ClassGroupDto MapToDto(ClassGroup cg)
    {
        return new ClassGroupDto(
            cg.Id,
            cg.Name,
            cg.Course?.Name ?? "N/A",
            cg.Teacher?.User != null ? $"{cg.Teacher.User.FirstName} {cg.Teacher.User.LastName}" : "N/A",
            cg.Enrollments.Count,
            cg.MaxCapacity,
            cg.AcademicTerm?.Name ?? "N/A"
        );
    }
}
