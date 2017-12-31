using CRMCore.Module.Entities;
using CRMCore.Module.CustomCollection.Entity.Content;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using SchemaModel = CRMCore.Module.CustomCollection.Entity.Schema;

namespace CRMCore.Module.CustomCollection.Entity
{
    /// <summary>
    /// https://eng.uber.com/schemaless-part-one/
    /// https://backchannel.org/blog/friendfeed-schemaless-mysql
    /// https://github.com/eklitzke/schemaless
    /// The name is Morphism because it can be anything :)
    /// </summary>
    public class Morphism : EntityBase
    {
        internal Morphism()
        {
        }

        public Morphism(string name, SchemaModel.Schema schema)
        {
            Name = name;
            _schema = JsonConvert.SerializeObject(schema);
        }

        public string Name { get; }

        public bool IsPublished { get; } = false;

        public bool IsDeleted { get; } = false;
        
        [NotMapped]
        public SchemaModel.Schema Schema
        {
            get
            {
                return JsonConvert.DeserializeObject<SchemaModel.Schema>(string.IsNullOrEmpty(_schema) ? "{}" : _schema);
            }
            /*set
            {
                _schema = JsonConvert.SerializeObject(value);
            } */
        }

        private string _schema;

        [NotMapped]
        public IdContentData Content
        {
            get
            {
                return JsonConvert.DeserializeObject<IdContentData>(string.IsNullOrEmpty(_content) ? "{}" : _content);
            }
            /*set
            {
                _content = JsonConvert.SerializeObject(value);
            } */
        }

        private string _content = "{}";
    }
}
