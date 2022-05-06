  using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using webapi;

namespace webapi_materiais.Servico
{
    public class AlunoServico
    {
        public static async Task<bool> ValidarUsuario(int id)
        {
            using var client = new HttpClient();
            
            client.DefaultRequestHeaders.Add("cookie", "some_cookie");

            using var response = await client.GetAsync($"/detalhes-aluno/{id}");
            
            return response.IsSuccessStatusCode;
        }
    }
}