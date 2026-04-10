namespace DataFlowHub.Application.Models.Auth;

public record LoginDto(
    string UserName,
    string Password
);