using FluentMigrator.Runner;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register Dependencies
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

// Run Migrations
using (var scope = app.Services.CreateScope())
{
    var migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    migrationRunner.MigrateUp();
}

// Configure Endpoints
app.MapControllers();
app.Run();
