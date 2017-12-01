using CRMCore.Framework.Entities.Helpers;
using CRMCore.Framework.Entities.Identity;
using CRMCore.Module.CustomCollection.Entity;
using CRMCore.Module.CustomCollection.Entity.Schema;
using CRMCore.Module.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                if (!context.Users.Any())
                {
                    await context.Users.AddRangeAsync(GetDefaultUser());
                }

                if (!context.Set<Morphism>().Any())
                {
                    await context.Set<Morphism>().AddRangeAsync(GetDefaultMorphisms());
                }

                if(!context.Set<Module.Task.Domain.Task>().Any())
                {
                    await context.Set<Module.Task.Domain.Task>().AddRangeAsync(GetDefaultTasks()); 
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
                Id = IdHelper.GenerateId("0fd266b3-4376-4fa3-9a35-aabe1d08043e"),
                Email = "demouser@nomail.com",
                LastName = "DemoLastName",
                FirstName = "DemoUser",
                PhoneNumber = "1234567890",
                UserName = "demouser@nomail.com",
                NormalizedEmail = "DEMOUSER@NOMAIL.COM",
                NormalizedUserName = "DEMOUSER@NOMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, "P@ssw0rd");

            return new List<ApplicationUser>()
            {
                user
            };
        }

        private IEnumerable<Morphism> GetDefaultMorphisms()
        {
            // Account
            var accountSchema = new Schema("account");

            accountSchema.Publish();

            accountSchema.Update(new SchemaProperties
            {
                Label = "Account",
                Hints = "Account entity in CRM-Core."
            });

            var account = new Morphism("account", accountSchema);

            // Contact
            var contactSchema = new Schema("contact");

            contactSchema.Publish();

            contactSchema.Update(new SchemaProperties
            {
                Label = "Contact",
                Hints = "Contact entity in CRM-Core."
            });

            contactSchema.AddField(new StringField(
                Guid.NewGuid(),
                "firstName",
                new StringFieldProperties
                {
                    Label = "FirstName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input first name.",
                    Placeholder = "Input first name...",
                    DefaultValue = "Phuong"
                }));

            contactSchema.AddField(new StringField(
                Guid.NewGuid(),
                "lastName",
                new StringFieldProperties
                {
                    Label = "LastName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input last name.",
                    Placeholder = "Input last name...",
                    DefaultValue = "Le"
                }));

            var contact = new Morphism("contact", contactSchema);

            // Customer
            var customerSchema = new Schema("customer");

            customerSchema.Publish();

            customerSchema.Update(new SchemaProperties
            {
                Label = "Customer",
                Hints = "Customer entity in CRM-Core."
            });

            customerSchema.AddField(new StringField(
                Guid.NewGuid(),
                "firstName",
                new StringFieldProperties
                {
                    Label = "FirstName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input first name.",
                    Placeholder = "Input first name...",
                    DefaultValue = "Lena"
                }));

            customerSchema.AddField(new StringField(
                Guid.NewGuid(),
                "lastName",
                new StringFieldProperties
                {
                    Label = "LastName",
                    IsRequired = true,
                    FieldType = StringFieldType.Input,
                    Hints = "Please input last name.",
                    Placeholder = "Input last name...",
                    DefaultValue = "Cao"
                }));

            var customer = new Morphism("customer", customerSchema);

            return new[] { account, contact, customer };
        }

        private IEnumerable<Module.Task.Domain.Task> GetDefaultTasks()
        {
            return new[] {
                Module.Task.Domain.Task.CreateInstance(
                    "Implementing Repository in Task module.", 
                    Module.Task.Domain.DueType.NextWeek, 
                    IdHelper.GenerateId("0fd266b3-4376-4fa3-9a35-aabe1d08043e"), 
                    Module.Task.Domain.CategoryType.Call),
                Module.Task.Domain.Task.CreateInstance(
                    "Implementing Controller in Task module.",
                    Module.Task.Domain.DueType.NextWeek,
                    IdHelper.GenerateId("0fd266b3-4376-4fa3-9a35-aabe1d08043e"),
                    Module.Task.Domain.CategoryType.FollowUp),
                Module.Task.Domain.Task.CreateInstance(
                    "Implementing front-end in Task module.",
                    Module.Task.Domain.DueType.NextWeek,
                    IdHelper.GenerateId("0fd266b3-4376-4fa3-9a35-aabe1d08043e"),
                    Module.Task.Domain.CategoryType.Meeting),
                Module.Task.Domain.Task.CreateInstance(
                    "Implementing Mobile in Task module.",
                    Module.Task.Domain.DueType.NextWeek,
                    IdHelper.GenerateId("0fd266b3-4376-4fa3-9a35-aabe1d08043e"),
                    Module.Task.Domain.CategoryType.Presentation)
            };
        }
    }
}
