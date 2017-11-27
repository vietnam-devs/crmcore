using System;
using Microsoft.AspNetCore.Identity;
using CRMCore.Framework.Entities.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using CRMCore.Module.Data;
using System.Collections.Generic;
using System.Linq;

namespace CRMCore.DBMigration.Console.Seeder
{
    public class ApplicationDbContextSeed
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

        public async Task SeedAsync(ApplicationDbContext context, IHostingEnvironment env, ILogger<ApplicationDbContextSeed> logger)
        {
            logger.LogInformation("Begin Seed data - Application DB context");
            try
            {
                if(!context.Users.Any()){
                    context.Users.AddRange(GetDefaultUser());
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, $"There is an error migrating data for ApplicationDbContext");
            }
        }

        private IEnumerable<ApplicationUser> GetDefaultUser()
        {
            var user = new ApplicationUser()
            {
                Email = "demouser@nomail.com",
                LastName = "DemoLastName",
                FirstName = "DemoUser",
                PhoneNumber = "1234567890",
                UserName = "demouser@nomail.com",
                NormalizedEmail = "DEMOUSER@NOMAIL.COM",
                NormalizedUserName = "DEMOUSER@NOMAIL.COM",
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, "P@ssw0rd");

            return new List<ApplicationUser>()
            {
                user
            };
        }
    }
}
