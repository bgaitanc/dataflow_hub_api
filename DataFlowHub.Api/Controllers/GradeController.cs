using System.Net;
using DataFlowHub.Api.Controllers.Base;
using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;
using DataFlowHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataFlowHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController(IGradeService gradeService) : DataFlowHubControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<GradeDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<GradeDto>> GetById(Guid id)
    {
        return await ExecuteServiceAsync(async () => await gradeService.GetByIdAsync(id));
    }

    [HttpGet]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<PagedResultDto<GradeDto>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PagedResultDto<GradeDto>>> GetPaged([FromQuery] GradeFilterDto filter)
    {
        return await ExecuteServiceAsync(async () => await gradeService.GetPagedAsync(filter));
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<Guid>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateGradeDto createDto)
    {
        return await ExecuteServiceAsync(async () => await gradeService.CreateAsync(createDto), HttpStatusCode.Created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<DataFlowHubGenericResponse>> Update(Guid id, [FromBody] UpdateGradeDto updateDto)
    {
        return await ExecuteServiceAsync(async () => await gradeService.UpdateAsync(id, updateDto));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<DataFlowHubGenericResponse>> Delete(Guid id)
    {
        return await ExecuteServiceAsync(async () => await gradeService.DeleteAsync(id));
    }
}
