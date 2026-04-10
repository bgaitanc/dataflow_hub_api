using System.Net;

namespace DataFlowHub.Domain.Exceptions;

public class NotFoundException(string name, object key) 
    : DataFlowHubException(HttpStatusCode.NotFound, $"Entity \"{name}\" ({key}) was not found.");
