using MongoDB.Bson.Serialization.Attributes;

namespace AdmUsuarios.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("salt")]
        public string Salt { get; set; }

        [BsonElement("login")]
        public string Login { get; set; }

        [BsonElement("senha")]
        public string Senha { get; set; }

        [BsonElement("token")]
        public string? Token { get; set; }
    }
}
