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

public class StudentService(
    IStudentRepository studentRepository,
    UserManager<ApplicationUser> userManager)
    : IStudentService
{
    public async Task<StudentDto> GetByIdAsync(Guid id)
    {
        var student = (await studentRepository.GetPagedAsync(
            1, 1, s => s.Id == id, null, "User")).Items.FirstOrDefault();

        if (student == null) throw new NotFoundException("Estudiante", id);

        return new StudentDto(
            student.Id,
            student.UserId,
            $"{student.User.FirstName} {student.User.LastName}",
            student.User.Email!,
            student.StudentCode,
            student.DateOfBirth
        );
    }

    public async Task<PagedResultDto<StudentDto>> GetPagedAsync(BaseFilterDto filter)
    {
        var (items, totalCount) = await studentRepository.GetPagedAsync(
            filter.PageNumber,
            filter.PageSize,
            null,
            q => q.OrderBy(s => s.User.LastName),
            "User"
        );

        return new PagedResultDto<StudentDto>
        {
            Items = items.Select(student => new StudentDto(
                student.Id,
                student.UserId,
                $"{student.User.FirstName} {student.User.LastName}",
                student.User.Email!,
                student.StudentCode,
                student.DateOfBirth
            )),
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<Guid> CreateAsync(CreateStudentDto createDto)
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

        await userManager.AddToRoleAsync(user, ApplicationRoles.Student);

        var student = new Student
        {
            UserId = user.Id,
            StudentCode = createDto.StudentCode,
            DateOfBirth = createDto.DateOfBirth,
            Address = createDto.Address
        };

        await studentRepository.AddAsync(student);
        await studentRepository.SaveChangesAsync();

        return student.Id;
    }

    public async Task UpdateAsync(Guid id, CreateStudentDto updateDto)
    {
        var student = await studentRepository.GetByIdAsync(id)
                      ?? throw new NotFoundException(nameof(Student), id);

        var user = await userManager.FindByIdAsync(student.UserId.ToString())
                   ?? throw new NotFoundException(nameof(ApplicationUser), student.UserId);

        user.FirstName = updateDto.FirstName;
        user.LastName = updateDto.LastName;
        
        // Note: Email update might require more complex logic (verification, etc.)
        // For simplicity, we update it if it's different and not taken.
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

        student.StudentCode = updateDto.StudentCode;
        student.DateOfBirth = updateDto.DateOfBirth;
        student.Address = updateDto.Address;

        studentRepository.Update(student);
        await studentRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var student = await studentRepository.GetByIdAsync(id)
                      ?? throw new NotFoundException(nameof(Student), id);

        var user = await userManager.FindByIdAsync(student.UserId.ToString());

        studentRepository.Delete(student);
        await studentRepository.SaveChangesAsync();

        if (user != null)
        {
            await userManager.DeleteAsync(user);
        }
    }
}
