using Microsoft.Extensions.Configuration;

namespace CRMCore.Module.Data.SqlServer
{
    public sealed class SqlServerDatabaseConnectionStringFactory : IDatabaseConnectionStringFactory
    {
        private readonly IConfiguration _config;

        public SqlServerDatabaseConnectionStringFactory(IConfiguration config)
        {
            _config = config;
        }

        public string Create()
        {
            return _config.GetConnectionString("SqlServerDefault");
        }
    }
}
