using MongoDB.Bson.Serialization.Attributes;

namespace AdmUsuarios.Models
{
    public class Estado
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nome")]
        public required string Nome { get; set; }

        [BsonElement("sigla")]
        public required string Sigla { get; set; }

    }
}
