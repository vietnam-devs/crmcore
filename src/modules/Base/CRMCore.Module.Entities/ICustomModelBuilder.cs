using Microsoft.EntityFrameworkCore;

namespace CRMCore.Module.Entities
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
