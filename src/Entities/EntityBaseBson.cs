using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class EntityBaseBson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string? Id { get; set; }

        [BsonElement("CreatedAt")]
        public virtual DateTime? CreatedAt { get; set; }

        [BsonElement("UpdatedAt")]
        public virtual DateTime? UpdatedAt { get; set; }

        [BsonElement("NotifiedIn")]
        public virtual DateTime? NotifiedIn { get; set; }
    }
}