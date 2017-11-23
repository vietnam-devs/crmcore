namespace CRMCore.Module.Schema.Entity
{
    public interface ISchemaEntity
    {
        string Name { get; }

        bool IsPublished { get; }

        bool IsDeleted { get; }

        Model.Schema SchemaModel { get; }
    }
}
