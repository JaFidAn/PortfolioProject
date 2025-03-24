using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Contexts.Data;

public class DbInitializer
{
    public static async Task SeedData(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        var adminEmail = "r.alagezov@gmail.com";
        var adminUserName = "rasimus";

        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var adminUser = new AppUser
            {
                FullName = "Rasim Alagezov",
                UserName = adminUserName,
                Email = adminEmail
            };

            var result = await userManager.CreateAsync(adminUser, "R@sim1984");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

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
