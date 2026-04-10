namespace DataFlowHub.Application.Models.Auth;

public record AuthResponseDto(
    Guid UserId,
    string Email,
    string UserName,
    string Token,
    string RefreshToken,
    IEnumerable<string> Roles
);