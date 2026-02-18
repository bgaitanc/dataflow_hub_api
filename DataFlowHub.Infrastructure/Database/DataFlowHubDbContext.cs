using System.Reflection;
using DataFlowHub.Domain.Entities.Academic;
using DataFlowHub.Domain.Entities.Actors;
using DataFlowHub.Domain.Entities.Base;
using DataFlowHub.Domain.Entities.Enums;
using DataFlowHub.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataFlowHub.Infrastructure.Database;

public class DataFlowHubDbContext(DbContextOptions<DataFlowHubDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<AcademicTerm> AcademicTerms { get; set; }
    public DbSet<ClassGroup> ClassGroups { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Grade> Grades { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasPostgresExtension("uuid-ossp");

        builder.HasPostgresEnum<EnrollmentStatus>();

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<ApplicationUser>().ToTable("Users", "identity");
        builder.Entity<IdentityRole<Guid>>().ToTable("Roles", "identity");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "identity");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", "identity");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "identity");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", "identity");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "identity");

        SeedRoles(builder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>().HaveColumnType("timestamp without time zone");
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var trackedEntities = GetTrackedEntities();

        var now = DateTime.Now;
        foreach (var entityEntry in trackedEntities)
        {
            if (entityEntry.Entity is not BaseEntity entity) continue;

            if (entityEntry.State == EntityState.Added)
            {
                entity.CreatedAt = now;
                Entry(entity).Property(x => x.CreatedAt).IsModified = true;
                Entry(entity).Property(x => x.UpdatedAt).IsModified = false;
                Entry(entity).Property(x => x.UpdatedBy).IsModified = false;
            }

            if (entityEntry.State != EntityState.Modified) continue;

            entity.UpdatedAt = now;
            Entry(entity).Property(x => x.UpdatedAt).IsModified = true;
            Entry(entity).Property(x => x.CreatedAt).IsModified = false;
            Entry(entity).Property(x => x.CreatedBy).IsModified = false;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private IEnumerable<EntityEntry> GetTrackedEntities()
    {
        var state = new List<EntityState>
        {
            EntityState.Added,
            EntityState.Modified
        };

        return ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && state.Any(s => e.State == s));
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("8bc2bfc1-1e12-4b5f-a1ff-679b06b1996b"), Name = "Admin", NormalizedName = "ADMIN",
                ConcurrencyStamp = "c735af80-37c2-434b-9c50-c8f3c5fd1be2"
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("15babe3a-44b5-4045-81b2-d1e4ef87ce32"), Name = "Teacher", NormalizedName = "TEACHER",
                ConcurrencyStamp = "5150d7aa-213d-42a4-945e-5d60c96c6485"
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("29026dd1-206d-4f54-849e-3aaa7422b07d"), Name = "Student", NormalizedName = "STUDENT",
                ConcurrencyStamp = "361075c0-576b-4d9c-89da-b059ae14d863"
            }
        );
    }
}