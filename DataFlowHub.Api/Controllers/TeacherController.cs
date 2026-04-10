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
public class TeacherController(ITeacherService teacherService) : DataFlowHubControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<TeacherDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<TeacherDto>> GetById(Guid id)
    {
        return await ExecuteServiceAsync(async () => await teacherService.GetByIdAsync(id));
    }

    [HttpGet]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<PagedResultDto<TeacherDto>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PagedResultDto<TeacherDto>>> GetPaged([FromQuery] BaseFilterDto filter)
    {
        return await ExecuteServiceAsync(async () => await teacherService.GetPagedAsync(filter));
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<Guid>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTeacherDto createDto)
    {
        return await ExecuteServiceAsync(async () => await teacherService.CreateAsync(createDto), HttpStatusCode.Created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<DataFlowHubGenericResponse>> Update(Guid id, [FromBody] CreateTeacherDto updateDto)
    {
        return await ExecuteServiceAsync(async () => await teacherService.UpdateAsync(id, updateDto));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<DataFlowHubGenericResponse>> Delete(Guid id)
    {
        return await ExecuteServiceAsync(async () => await teacherService.DeleteAsync(id));
    }
}
