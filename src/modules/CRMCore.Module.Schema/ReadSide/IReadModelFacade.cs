using CRMCore.Module.Schema.Features.GetSchemaItems.Dtos;
using System.Collections.Generic;

namespace CRMCore.Module.Schema.ReadSide
{
    public interface IReadModelFacade
    {
        IEnumerable<SchemaDto> GetSchemaItems();
    }
}
