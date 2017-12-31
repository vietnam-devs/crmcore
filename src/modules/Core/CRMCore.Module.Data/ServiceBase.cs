using CRMCore.Module.Entities;

namespace CRMCore.Module.Data
{
    public abstract class QueryServiceBase : IService
    {
    }

    public abstract class CommandServiceBase : IService
    {
        protected readonly IUnitOfWorkAsync UnitOfWork;
        protected CommandServiceBase(IUnitOfWorkAsync uow)
        {
            UnitOfWork = uow;
        }
    }
}
