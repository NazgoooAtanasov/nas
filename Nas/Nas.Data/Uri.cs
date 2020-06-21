using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nas.Data
{
    public class Uri
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Link { get; set; }
        public string Slug { get; set; }
    }
}