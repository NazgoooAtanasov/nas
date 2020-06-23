using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nas.Data
{
    public class Uri
    {
        public string Link { get; set; }
        public string Slug { get; set; }
    }
}