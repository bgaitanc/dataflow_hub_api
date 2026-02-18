using DataFlowHub.Domain.Entities.Actors;
using Microsoft.AspNetCore.Identity;

namespace DataFlowHub.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public Student? StudentProfile { get; set; }
    public Teacher? TeacherProfile { get; set; }
}