using System.Net;
using DataFlowHub.Application.Models.Actors;
using DataFlowHub.Application.Models.Common;
using DataFlowHub.Application.Services.Interfaces;
using DataFlowHub.Domain.Entities.Actors;
using DataFlowHub.Domain.Entities.Identity;
using DataFlowHub.Domain.Exceptions;
using DataFlowHub.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DataFlowHub.Application.Services.Implementation;

public class TeacherService(
    ITeacherRepository teacherRepository,
    UserManager<ApplicationUser> userManager)
    : ITeacherService
{
    public async Task<TeacherDto> GetByIdAsync(Guid id)
    {
        var teacher = (await teacherRepository.GetPagedAsync(
            1, 1, t => t.Id == id, null, "User")).Items.FirstOrDefault();

        if (teacher == null) throw new NotFoundException("Docente", id);

        return new TeacherDto(
            teacher.Id,
            $"{teacher.User.FirstName} {teacher.User.LastName}",
            teacher.User.Email!,
            teacher.EmployeeNumber,
            teacher.Specialization
        );
    }

    public async Task<PagedResultDto<TeacherDto>> GetPagedAsync(BaseFilterDto filter)
    {
        var (items, totalCount) = await teacherRepository.GetPagedAsync(
            filter.PageNumber,
            filter.PageSize,
            null,
            q => q.OrderBy(t => t.User.LastName),
            "User"
        );

        return new PagedResultDto<TeacherDto>
        {
            Items = items.Select(teacher => new TeacherDto(
                teacher.Id,
                $"{teacher.User.FirstName} {teacher.User.LastName}",
                teacher.User.Email!,
                teacher.EmployeeNumber,
                teacher.Specialization
            )),
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<Guid> CreateAsync(CreateTeacherDto createDto)
    {
        var userExists = await userManager.FindByEmailAsync(createDto.Email);
        if (userExists != null)
            throw new DomainException("El email ya está registrado.");

        var user = new ApplicationUser
        {
            UserName = createDto.Email,
            Email = createDto.Email,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName
        };

        var result = await userManager.CreateAsync(user, createDto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new DomainException($"Error al crear usuario: {errors}");
        }

        await userManager.AddToRoleAsync(user, ApplicationRoles.Teacher);

        var teacher = new Teacher
        {
            UserId = user.Id,
            EmployeeNumber = createDto.EmployeeNumber,
            Specialization = createDto.Specialization,
            Title = createDto.Title
        };

        await teacherRepository.AddAsync(teacher);
        await teacherRepository.SaveChangesAsync();

        return teacher.Id;
    }

    public async Task UpdateAsync(Guid id, CreateTeacherDto updateDto)
    {
        var teacher = await teacherRepository.GetByIdAsync(id)
                      ?? throw new NotFoundException(nameof(Teacher), id);

        var user = await userManager.FindByIdAsync(teacher.UserId.ToString())
                   ?? throw new NotFoundException(nameof(ApplicationUser), teacher.UserId);

        user.FirstName = updateDto.FirstName;
        user.LastName = updateDto.LastName;

        if (user.Email != updateDto.Email)
        {
            var emailExists = await userManager.FindByEmailAsync(updateDto.Email);
            if (emailExists != null)
                throw new DomainException("El nuevo email ya está registrado.");

            user.Email = updateDto.Email;
            user.UserName = updateDto.Email;
        }

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new DomainException($"Error al actualizar usuario: {errors}");
        }

        teacher.EmployeeNumber = updateDto.EmployeeNumber;
        teacher.Specialization = updateDto.Specialization;
        teacher.Title = updateDto.Title;

        teacherRepository.Update(teacher);
        await teacherRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var teacher = await teacherRepository.GetByIdAsync(id)
                      ?? throw new NotFoundException(nameof(Teacher), id);

        var user = await userManager.FindByIdAsync(teacher.UserId.ToString());

        teacherRepository.Delete(teacher);
        await teacherRepository.SaveChangesAsync();

        if (user != null)
        {
            await userManager.DeleteAsync(user);
        }
    }
}
