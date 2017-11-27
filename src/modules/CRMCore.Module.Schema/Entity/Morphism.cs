using CRMCore.Framework.Entities;
using CRMCore.Framework.Entities.Content;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMCore.Module.Schema.Entity
{
    /// <summary>
    /// https://eng.uber.com/schemaless-part-one/
    /// https://backchannel.org/blog/friendfeed-schemaless-mysql
    /// https://github.com/eklitzke/schemaless
    /// 
    /// The name is Morphism because it can be anything :)
    /// </summary>
    public class Morphism : BaseEntity
    {
        internal Morphism()
        {
        }

        public Morphism(string name, Framework.Entities.Schema.Schema schema)
        {
            Name = name;
            _schema = schema.ToString();
        }

        public string Name { get; }

        public bool IsPublished { get; } = false;

        public bool IsDeleted { get; } = false;
        
        [NotMapped]
        public Framework.Entities.Schema.Schema Schema
        {
            get
            {
                return JsonConvert.DeserializeObject<Framework.Entities.Schema.Schema>(string.IsNullOrEmpty(_schema) ? "{}" : _schema);
            }
            set
            {
                _schema = JsonConvert.SerializeObject(value);
            }
        }

        private string _schema;
        // public Framework.Entities.Schema.Schema Schema { get; }
        
        [NotMapped]
        public IdContentData Content
        {
            get
            {
                return JsonConvert.DeserializeObject<IdContentData>(string.IsNullOrEmpty(_content) ? "{}" : _content);
            }
            set
            {
                _content = JsonConvert.SerializeObject(value);
            }
        }

        private string _content;
        // public IdContentData Content { get; }
    }
}
