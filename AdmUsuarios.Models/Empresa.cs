using MongoDB.Bson.Serialization.Attributes;


namespace AdmUsuarios.Models
{
    public class Empresa
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nome")]
        public required string Nome { get; set; }

        [BsonElement("cnpj")]
        public required string CNPJ { get; set; }

        public Usuario Usuario { get; set; }

        public Logradouro Logradouro { get; set; }
    }
}
