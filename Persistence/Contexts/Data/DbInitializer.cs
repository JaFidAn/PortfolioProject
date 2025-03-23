using Domain.Entities;

namespace Persistence.Contexts.Data;

public class DbInitializer
{
    public static async Task SeedData(ApplicationDbContext context)
    {
        if (!context.Technologies.Any())
        {
            var technologies = new List<Technology>
            {
                new() { Name = "ASP.NET Core" },
                new() { Name = "Entity Framework Core" },
                new() { Name = "AutoMapper" },
                new() { Name = "MediatR" },
                new() { Name = "FluentValidation" }
            };

            context.Technologies.AddRange(technologies);
            await context.SaveChangesAsync();
        }
    }
}
