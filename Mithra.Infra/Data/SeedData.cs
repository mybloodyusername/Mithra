using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mithra.Domain.Entities;
using Mithra.Domain.Enums;

namespace Mithra.Infra.Data;

public static class SeedData
{
    public static async Task Initialize(
        ILogger<MithraDbContext> logger,
        IServiceProvider serviceProvider,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        using var scope = serviceProvider.CreateScope();

        var context = serviceProvider.GetRequiredService<MithraDbContext>();

        foreach (var role in Enum.GetValues<UserRole>())
        {
            var roleName = role.ToString();
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                logger.LogInformation("Role created successfully: {RoleName}", roleName);
            }
            else
            {
                logger.LogInformation("Role already exists: {RoleName}", roleName);
            }
        }

        var adminEmail = "admin@mithra.com";
        var adminPassword = "Admin@123";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, nameof(UserRole.Admin));
                logger.LogInformation("Admin created successfully!");
            }
            else
            {
                throw new Exception("Failed to create admin user: " +
                                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
        else
        {
            logger.LogInformation("Admin already exists!");
        }

        await context.SaveChangesAsync();
    }
}