using Application;
using Persistence;
using API.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Application.Features.Projects.Validators;
using Persistence.Contexts;
using Persistence.Contexts.Data;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Modular Swagger
builder.Services.AddSwaggerDocumentation();

// Register Application & AddPersistence
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

// Register FluentValidation 
builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Add Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(allowIntegerValues: false));
    });

// Register Middleware
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Swagger Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

// Use Global Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Map Controllers
app.MapControllers();

// Apply migrations and seed data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration and seeding");
}

app.Run();
