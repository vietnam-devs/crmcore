using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CRMCore.Module.GraphQL
{
    public static class DbContextExtensions
    {
        public static IQueryable Query(
            this DbContext context,
            string entityName) =>
                context.Query(context.Model.FindEntityType(entityName).ClrType);

        public static IQueryable Query(
            this DbContext context, 
            Type entityType) =>
                (IQueryable)SetMethod.MakeGenericMethod(entityType).Invoke(context, null);

        public static IQueryable Query(
            this DbContext context,
            string entityName,
            string[] includes)
        {
            return context.DbSetQuery(
                context.Model.FindEntityType(entityName).ClrType, 
                includes);
        }

        public static IQueryable DbSetQuery(
            this DbContext context,
            Type entityType,
            string[] includes)
        {
            return context.Set(entityType, includes);
        }

        public static IQueryable Set(this DbContext context, 
            Type T,
            string[] includes)
        {
            // Get the generic type definition
            MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);

            // Build a method with the specific type argument you're interested in
            method = method.MakeGenericMethod(T);

            var dbSet = method.Invoke(context, null);

            if (includes != null && includes.Count() > 0)
            {
                // https://stackoverflow.com/questions/38312437/can-a-string-based-include-alternative-be-created-in-entity-framework-core
                MethodInfo includeMethodInfo = typeof(EntityFrameworkQueryableExtensions)
                    .GetTypeInfo()
                    .GetDeclaredMethods(
                        nameof(EntityFrameworkQueryableExtensions.Include))
                            .FirstOrDefault(mi => mi.GetParameters().Any(pi => pi.Name == "navigationPropertyPath"));

                foreach (var include in includes)
                {
                    var parameter = Expression.Parameter(T, "e");
                    var property = Expression.PropertyOrField(parameter, include);
                    includeMethodInfo = includeMethodInfo.MakeGenericMethod(T, property.Type);

                    dbSet = includeMethodInfo.Invoke(
                        null,
                        new object[] { dbSet, Expression.Lambda(property, new[] { parameter }) });
                }
            }

            return dbSet as IQueryable;
        }

        static readonly MethodInfo SetMethod = typeof(DbContext).GetMethod(nameof(DbContext.Set));
    }
}
