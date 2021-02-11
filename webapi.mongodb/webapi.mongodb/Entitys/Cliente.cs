using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace webapi.mongodb.Entitys
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get; set; }
        public ICollection<Telefone> Telefone { get; set; }

        [BsonElement("Endereco")]
        public Endereco EnderecoCliente { get; set; }
    }
}
