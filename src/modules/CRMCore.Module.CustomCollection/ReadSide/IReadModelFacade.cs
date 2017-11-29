using CRMCore.Module.CustomCollection.Dtos;
using System.Collections.Generic;

namespace CRMCore.Module.CustomCollection.ReadSide
{
    public interface IReadModelFacade
    {
        IEnumerable<SchemaDto> GetSchemaItems();
        SchemaDetailsItemDto GetSchemaDetailsItem(string name);
    }
}
