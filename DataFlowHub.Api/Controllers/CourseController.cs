using System.Net;
using DataFlowHub.Api.Controllers.Base;
using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;
using DataFlowHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataFlowHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService courseService) : DataFlowHubControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<CourseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CourseDto>> GetById(Guid id)
    {
        return await ExecuteServiceAsync(async () => await courseService.GetByIdAsync(id));
    }

    [HttpGet]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<PagedResultDto<CourseDto>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PagedResultDto<CourseDto>>> GetPaged([FromQuery] CourseFilterDto filter)
    {
        return await ExecuteServiceAsync(async () => await courseService.GetPagedAsync(filter));
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<Guid>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateCourseDto createDto)
    {
        return await ExecuteServiceAsync(async () => await courseService.CreateAsync(createDto), HttpStatusCode.Created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<DataFlowHubGenericResponse>> Update(Guid id, [FromBody] CreateCourseDto updateDto)
    {
        return await ExecuteServiceAsync(async () => await courseService.UpdateAsync(id, updateDto));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<DataFlowHubGenericResponse>> Delete(Guid id)
    {
        return await ExecuteServiceAsync(async () => await courseService.DeleteAsync(id));
    }
}
