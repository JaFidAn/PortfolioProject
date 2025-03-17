using System.Reflection;
using Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using Application.Mapping;
using Persistence.Repositories;
using Persistence.Migrations;
using Persistence.Cqrs.Commands;
using Persistence.Cqrs.Queries;
using Application.Common.Interfaces;
using Infrastructure.Logging;
using Application.Services.Concretes;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("SqlServerConnection")
            ?? throw new InvalidOperationException("Connection string 'SqlConnectionString' is missing in appsettings.json");

        services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));

        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetAssembly(typeof(ProjectRepository))!)
            .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetAssembly(typeof(ProjectCommand))!)
            .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Command")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetAssembly(typeof(ProjectQuery))!)
            .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Query")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetAssembly(typeof(ProjectService))!)
            .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Service")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(_20250306135801_CreateProjectsTable).Assembly).For.Migrations());

        services.AddAutoMapper(typeof(MappingProfiles));

        return services;
    }
}
