using CRMCore.Module.Schema.Features.GetSchemaItems.Dtos;
using CRMCore.Module.Schema.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace CRMCore.Module.Schema.ReadSide.Impl
{
    public partial class ReadModelFacade : IReadModelFacade
    {
        public IEnumerable<SchemaDto> GetSchemaItems()
        {
            return InMemoryDatabase.SchemaItems.Select(x =>
            {
                return new SchemaDto
                {
                    Id = x.Key,
                    IsPublished = x.Value.IsPublished,
                    Name = x.Value.Name,
                    Version = 1,
                    Properties = new SchemaPropertiesDto
                    {
                        Label = x.Value.Properties.Label,
                        Hints = x.Value.Properties.Hints
                    }
                };
            });
        }
    }
}
