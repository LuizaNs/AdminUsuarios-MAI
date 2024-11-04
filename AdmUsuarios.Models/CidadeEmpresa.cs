using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmUsuarios.Models
{
    public class CidadeEmpresa
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CidadeId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string EmpresaId { get; set; }
        public float Label { get; set; }
    }
}
