using System.Collections.Generic;

namespace CRMCore.Module.Schema
{
    public interface IReadModelFacade
    {
        IEnumerable<Domain.Schema> GetSchemaItems();
    }
}
