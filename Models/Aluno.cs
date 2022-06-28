namespace webapi.Models
{
    public partial class Aluno
    {
        #region "Propriedades"
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string Notas { get; set; }

        #endregion
    }
}
