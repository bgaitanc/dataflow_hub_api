using System.Net;

namespace DataFlowHub.Domain.Exceptions;

public class DataFlowHubException(HttpStatusCode statusCode, string message) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}