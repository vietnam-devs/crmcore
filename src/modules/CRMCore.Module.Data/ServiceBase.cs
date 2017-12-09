using CRMCore.Framework.Entities;

namespace CRMCore.Module.Data
{
    public abstract class ServiceBase : IService
    {
        protected readonly IUnitOfWorkAsync UnitOfWork;
        protected ServiceBase(IUnitOfWorkAsync uow)
        {
            UnitOfWork = uow;
        }
    }
}
