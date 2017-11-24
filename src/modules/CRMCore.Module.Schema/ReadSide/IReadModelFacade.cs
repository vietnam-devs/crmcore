using CRMCore.Module.Schema.Dtos;
using System.Collections.Generic;

namespace CRMCore.Module.Schema.ReadSide
{
    public interface IReadModelFacade
    {
        IEnumerable<SchemaDto> GetSchemaItems();
        SchemaDetailsItemDto GetSchemaDetailsItem(string name);
    }
}
