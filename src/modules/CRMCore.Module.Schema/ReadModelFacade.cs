using CRMCore.Module.Schema.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace CRMCore.Module.Schema
{
    public class ReadModelFacade : IReadModelFacade
    {
        public IEnumerable<Domain.Schema> GetSchemaItems()
        {
            return InMemoryDatabase.SchemaItems.Select(x => x.Value);
        }
    }
}
