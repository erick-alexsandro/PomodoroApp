using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoApp.Models
{
    public class TodoItem
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("task")]
        public required string Task { get; set; }

        [BsonElement("done")]
        public bool Done { get; set; }
    }
}