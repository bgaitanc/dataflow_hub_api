using DataFlowHub.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Npgsql.NameTranslation;

namespace DataFlowHub.Infrastructure.Database;

public class DataFlowHubDbContextFactory : IDesignTimeDbContextFactory<DataFlowHubDbContext>
{
    public DataFlowHubDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        var apiPath = Path.Combine(basePath, "../DataFlowHub.Api");

        if (!Directory.Exists(apiPath))
        {
            apiPath = Path.Combine(basePath, "DataFlowHub.Api");
        }

        var builder = new ConfigurationBuilder()
            .SetBasePath(apiPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .AddUserSecrets<DataFlowHubDbContextFactory>();
        
        var configuration = builder.Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                "No se encontró 'ConnectionStrings:DefaultConnection' en el appsettings.json de la API ni en variables de entorno.");
        }

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        
        dataSourceBuilder.MapEnum<EnrollmentStatus>(
            pgName: "enrollment_status", 
            nameTranslator: new NpgsqlSnakeCaseNameTranslator()
        );
        
        var dataSource = dataSourceBuilder.Build();

        var optionsBuilder = new DbContextOptionsBuilder<DataFlowHubDbContext>();
        optionsBuilder.UseNpgsql(dataSource, opts => 
        {
            opts.MigrationsAssembly("DataFlowHub.Infrastructure");
        });

        return new DataFlowHubDbContext(optionsBuilder.Options);
    }
}