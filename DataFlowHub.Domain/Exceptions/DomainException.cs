using System.Net;

namespace DataFlowHub.Domain.Exceptions;

public class DomainException(string message) : DataFlowHubException(HttpStatusCode.BadRequest, message);
