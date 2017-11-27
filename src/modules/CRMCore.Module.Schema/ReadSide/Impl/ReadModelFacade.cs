using CRMCore.Module.Schema.Dtos;
using CRMCore.Module.Schema.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace CRMCore.Module.Schema.ReadSide.Impl
{
    public partial class ReadModelFacade : IReadModelFacade
    {
        public SchemaDetailsItemDto GetSchemaDetailsItem(string name)
        {
            var schemaKeyValue = InMemoryDatabase
                .SchemaItems
                .FirstOrDefault(x => x.Value.Name == name);

            return new SchemaDetailsItemDto {
                Id = schemaKeyValue.Key,
                Name = schemaKeyValue.Value.Name,
                Version = 1,
                IsPublished = schemaKeyValue.Value.IsPublished,
                Properties = new SchemaPropertiesDto
                {
                    Label = schemaKeyValue.Value.Schema.Properties.Label,
                    Hints = schemaKeyValue.Value.Schema.Properties.Hints
                },
                Fields = schemaKeyValue.Value.Schema.Fields.Select(x=> new FieldDto {
                    FieldId = x.Id,
                    Name = x.Name,
                    IsDisabled = x.IsDisabled,
                    IsHidden = x.IsHidden,
                    IsLocked = x.IsLocked,
                    Properties = new StringFieldPropertiesDto {
                        Label = x.RawProperties.Label,
                        IsListField = x.RawProperties.IsListField,
                        IsRequired = x.RawProperties.IsRequired,
                        Placeholder = x.RawProperties.Placeholder,
                        Hints = x.RawProperties.Hints
                    }
                }).ToList()
            };
        }

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
                        Label = x.Value.Schema.Properties.Label,
                        Hints = x.Value.Schema.Properties.Hints
                    }
                };
            });
        }
    }
}
