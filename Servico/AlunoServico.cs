  using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using webapi;

namespace webapi_materiais.Servico
{
    public class AlunoServico
    {
        public static async Task<HttpStatusCode> ValidarUsuario(int id)
        {
            using var client = new HttpClient();

            try
            {
                using var response = await client.GetAsync($"{Program.AlunosAPI}/detalhes-aluno/{id}");
                return response.StatusCode;    
            }
            catch
            {
                return  HttpStatusCode.InternalServerError;
            }
        }
    }
}