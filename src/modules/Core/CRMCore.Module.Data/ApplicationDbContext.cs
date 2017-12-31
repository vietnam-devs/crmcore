using CRMCore.Module.Entities;
using CRMCore.Module.Entities.Identity;
using CRMCore.Module.MvcCore.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CRMCore.Module.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var typeToRegisters = new List<Type>();

            var ourModules = "CRMCore.Module.*".LoadAssemblyWithPattern();

            typeToRegisters.AddRange(ourModules.SelectMany(m => m.DefinedTypes));

            RegisterEntities(builder, typeToRegisters);

            RegisterConvention(builder);

            base.OnModelCreating(builder);

            RegisterCustomMappings(builder, typeToRegisters);
        }

        private static void RegisterEntities(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            // TODO: will optimize this more
            var types = typeToRegisters.Where(x => typeof(IEntity).IsAssignableFrom(x) &&
                !x.GetTypeInfo().IsAbstract);

            foreach(var type in types)
            {
                modelBuilder.Entity(type);
            }
        }

        private static void RegisterConvention(ModelBuilder modelBuilder)
        {
            var types = modelBuilder.Model.GetEntityTypes().Where(entity => entity.ClrType.Namespace != null);

            foreach(var entityType in types)
            {
                var tablePrefix = "crm";
                if (!entityType.ClrType.AssemblyQualifiedName.Contains("CRMCore.Module"))
                {
                    tablePrefix = entityType.ClrType.Namespace.Split('.')[2];
                }

                var tableName = string.Concat(tablePrefix, "_", entityType.ClrType.Name, "s");
                modelBuilder.Entity(entityType.Name).ToTable(tableName);
            }
        }

        private static void RegisterCustomMappings(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            var customModelBuilderTypes = typeToRegisters.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x));

            foreach (var builderType in customModelBuilderTypes)
            {
                if (builderType != null && builderType != typeof(ICustomModelBuilder))
                {
                    var builder = (ICustomModelBuilder)Activator.CreateInstance(builderType);
                    builder.Build(modelBuilder);
                }
            }
        }
    }
}
