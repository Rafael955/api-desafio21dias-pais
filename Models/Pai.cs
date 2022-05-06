using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webapi.Models
{
    public partial class Pai
    {
        #region "Propriedades"

        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        [BsonRequired()]
        public string Nome { get; set; }

        [BsonElement("aluno_id")]
        [BsonRequired()]
        public int AlunoId { get; set; }

        #endregion
    }
}
