using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;
using DataFlowHub.Application.Services.Interfaces;
using DataFlowHub.Domain.Entities.Academic;
using DataFlowHub.Domain.Exceptions;
using DataFlowHub.Domain.Repositories.Interfaces;

namespace DataFlowHub.Application.Services.Implementation;

public class CourseService(ICourseRepository courseRepository) : ICourseService
{
    public async Task<CourseDto> GetByIdAsync(Guid id)
    {
        var course = await courseRepository.GetByIdAsync(id)
                     ?? throw new NotFoundException(nameof(Course), id);

        return new CourseDto(
            course.Id,
            course.Name,
            course.Code,
            course.Credits,
            course.Description,
            course.IsActive
        );
    }

    public async Task<PagedResultDto<CourseDto>> GetPagedAsync(CourseFilterDto filter)
    {
        var (items, totalCount) = await courseRepository.GetPagedAsync(
            filter.PageNumber,
            filter.PageSize,
            c => (string.IsNullOrEmpty(filter.Name) || c.Name.Contains(filter.Name)) &&
                 (string.IsNullOrEmpty(filter.Code) || c.Code.Contains(filter.Code)),
            q => q.OrderBy(c => c.Name)
        );

        return new PagedResultDto<CourseDto>
        {
            Items = items.Select(c => new CourseDto(
                c.Id,
                c.Name,
                c.Code,
                c.Credits,
                c.Description,
                c.IsActive
            )),
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<Guid> CreateAsync(CreateCourseDto createDto)
    {
        var course = new Course
        {
            Name = createDto.Name,
            Code = createDto.Code,
            Credits = createDto.Credits,
            Description = createDto.Description
        };

        await courseRepository.AddAsync(course);
        await courseRepository.SaveChangesAsync();

        return course.Id;
    }

    public async Task UpdateAsync(Guid id, CreateCourseDto updateDto)
    {
        var course = await courseRepository.GetByIdAsync(id)
                     ?? throw new NotFoundException(nameof(Course), id);

        course.Name = updateDto.Name;
        course.Code = updateDto.Code;
        course.Credits = updateDto.Credits;
        course.Description = updateDto.Description;

        courseRepository.Update(course);
        await courseRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var course = await courseRepository.GetByIdAsync(id)
                     ?? throw new NotFoundException(nameof(Course), id);

        courseRepository.Delete(course);
        await courseRepository.SaveChangesAsync();
    }
}
