using System.Net;

namespace DataFlowHub.Api.Controllers.Base;

public class DataFlowHubGenericResponse
{
    public string? Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public bool Success { get; set; }
}

public class DataFlowHubGenericResponse<T> : DataFlowHubGenericResponse
{
    public required T Data { get; set; }
}