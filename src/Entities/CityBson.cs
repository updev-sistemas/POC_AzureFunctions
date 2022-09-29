using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class CityBson
    {
        [BsonElement("Name")]
        public virtual string? Name { get; set; }
    }
}