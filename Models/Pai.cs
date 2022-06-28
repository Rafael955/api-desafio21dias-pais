using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace webapi.Models
{
    public partial class Pai
    {
        #region "Propriedades"

        [BsonId()]
        [JsonIgnore]
        public ObjectId Codigo { get; set; }

        public string Id 
        { 
            get => this.Codigo.ToString(); 
            set
            {
                this.Codigo = ObjectId.Parse(value); 
            } 
        }
        
        [BsonElement("name")]
        [BsonRequired()]
        public string Nome { get; set; }

        [BsonElement("aluno_id")]
        [BsonRequired()]
        public int AlunoId { get; set; }

        public Aluno Aluno { get; set; }

        #endregion
    }
}
