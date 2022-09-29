using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class StateBson : EntityBaseBson
    {
        [BsonElement("Name")]
        public virtual string? Name { get; set; }

        [BsonElement("Uf")]
        public virtual string? Uf { get; set; }

        [BsonElement("Cities")]
        public virtual IEnumerable<CityBson>? Cities { get; set; }
    }
}