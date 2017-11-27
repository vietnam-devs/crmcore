using Microsoft.EntityFrameworkCore;

namespace CRMCore.Framework.Entities
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
