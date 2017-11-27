using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Models;

namespace CRMCore.DBMigration.Console.Seeder
{
    public class ConfigurationDbContextSeed
    {
        public async Task SeedAsync(ConfigurationDbContext context)
        {

            //await context.ApiResources.AddRangeAsync(GetApis().Where(x=> !context.ApiResources.Any(y => y.Name == x.Name)));           
            await context.SaveChangesAsync();
        }

        private IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("Notifications", "Notifications Service")
            };
        }
    }
}
