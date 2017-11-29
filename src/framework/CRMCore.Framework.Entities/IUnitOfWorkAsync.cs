using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace CRMCore.Framework.Entities
{
    public interface IUnitOfWorkAsync
    {
        IRepositoryAsync<TEntity> Repository<TEntity>() where TEntity : EntityBase;

        int SaveChanges();

        int ExecuteSqlCommand(string sql, params object[] parameters);

        int? CommandTimeout { get; set; }

        Task BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        bool Commit();

        void Rollback();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);

        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters);
    }
}
