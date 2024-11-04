using MongoDB.Bson.Serialization.Attributes;

namespace AdmUsuarios.Models
{
    public class Logradouro
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("cep")]
        public string Cep { get; set; }

        public Cidade Cidade { get; set; }
    }
}
