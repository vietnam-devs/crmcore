using Microsoft.EntityFrameworkCore;

namespace CRMCore.Module.Data.Mapping
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
