using MongoDB.Bson.Serialization.Attributes;

namespace AdmUsuarios.Models
{
    public class Cidade
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nome")]
        public string Nome { get; set; }

        public Estado Estado { get; set; }
    }
}

