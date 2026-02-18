namespace DataFlowHub.Application.Models.Auth;

public record LoginDto(
    string Email,
    string Password
);