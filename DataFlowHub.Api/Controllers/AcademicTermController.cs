using System.Net;
using DataFlowHub.Api.Controllers.Base;
using DataFlowHub.Application.Models.Academic;
using DataFlowHub.Application.Models.Common;
using DataFlowHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataFlowHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AcademicTermController(IAcademicTermService academicTermService) : DataFlowHubControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<AcademicTermDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<AcademicTermDto>> GetById(Guid id)
    {
        return await ExecuteServiceAsync(async () => await academicTermService.GetByIdAsync(id));
    }

    [HttpGet]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<PagedResultDto<AcademicTermDto>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PagedResultDto<AcademicTermDto>>> GetPaged([FromQuery] AcademicTermFilterDto filter)
    {
        return await ExecuteServiceAsync(async () => await academicTermService.GetPagedAsync(filter));
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse<Guid>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateAcademicTermDto createDto)
    {
        return await ExecuteServiceAsync(async () => await academicTermService.CreateAsync(createDto), HttpStatusCode.Created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<DataFlowHubGenericResponse>> Update(Guid id, [FromBody] UpdateAcademicTermDto updateDto)
    {
        return await ExecuteServiceAsync(async () => await academicTermService.UpdateAsync(id, updateDto));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DataFlowHubGenericResponse), (int)HttpStatusCode.NoContent)]
    public async Task<ActionResult<DataFlowHubGenericResponse>> Delete(Guid id)
    {
        return await ExecuteServiceAsync(async () => await academicTermService.DeleteAsync(id));
    }
}
