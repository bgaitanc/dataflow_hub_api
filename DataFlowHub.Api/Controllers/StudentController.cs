using System.Net;
using DataFlowHub.Api.Controllers.Base;
using DataFlowHub.Application.Models.Actors;
using DataFlowHub.Application.Models.Common;
using DataFlowHub.Application.Services.Interfaces;
using DataFlowHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DataFlowHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService studentService) : DataFlowHubControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<StudentDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<StudentDto>> GetById(Guid id)
    {
        return await ExecuteServiceAsync(async () => await studentService.GetByIdAsync(id));

    }

    [HttpGet]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<PagedResultDto<StudentDto>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PagedResultDto<StudentDto>>> GetPaged([FromQuery] BaseFilterDto filter)
    {
        return await ExecuteServiceAsync(async () => await studentService.GetPagedAsync(filter));
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<Guid>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateStudentDto createDto)
    {
        return await ExecuteServiceAsync(async () => await studentService.CreateAsync(createDto), HttpStatusCode.Created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<DataFlowHubGenericResponse>> Update(Guid id, [FromBody] CreateStudentDto updateDto)
    {
        return await ExecuteServiceAsync(async () => await studentService.UpdateAsync(id, updateDto));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<DataFlowHubGenericResponse>> Delete(Guid id)
    {
        return await ExecuteServiceAsync(async () => await studentService.DeleteAsync(id));
    }
}
