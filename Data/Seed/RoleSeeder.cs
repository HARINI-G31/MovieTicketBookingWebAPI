
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Constants;
using MovieTicketBookingWebAPI.Models;

namespace MovieTicketBookingWebAPI.Data.Seed
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            var passwordHasher = scope.ServiceProvider
                .GetRequiredService<IPasswordHasher<User>>();

            var adminPassword = configuration["SeedUsers:AdminPassword"];
            var managerPassword = configuration["SeedUsers:ManagerPassword"];

            if (string.IsNullOrWhiteSpace(adminPassword) ||
                string.IsNullOrWhiteSpace(managerPassword))
            {
                throw new InvalidOperationException(
                    "Seed user passwords are missing from User Secrets.");
            }

            await CreateUserIfNotExistsAsync(
                context, passwordHasher,
                "System Administrator",
                "admin@example.com",
                adminPassword,
                UserRoles.Admin);

            await CreateUserIfNotExistsAsync(
                context, passwordHasher,
                "Theatre Manager",
                "manager@example.com",
                managerPassword,
                UserRoles.TheatreManager);
        }

        private static async Task CreateUserIfNotExistsAsync(
            ApplicationDbContext context,
            IPasswordHasher<User> passwordHasher,
            string fullName,
            string email,
            string password,
            string role)
        {
            var existingUser = await context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (existingUser != null)
                return;

            var user = new User
            {
                FullName = fullName,
                Email = email,
                Role = role,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = passwordHasher.HashPassword(
                user, password);

            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
    }
}
