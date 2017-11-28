using CRMCore.Framework.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRMCore.Module.Schema.Entity
{
    public class MorphismModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Morphism>()
                .Property<string>("SchemaStr")
                .HasField("_schema");

            modelBuilder.Entity<Morphism>()
                .Property<string>("ContentStr")
                .HasField("_content");
        }
    }
}
