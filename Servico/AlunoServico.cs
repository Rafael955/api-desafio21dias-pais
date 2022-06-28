using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using webapi;
using webapi.Models;

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

        public static async Task<Aluno> BuscarPorId(int id)
        {
            using var client = new HttpClient();

            try
            {
                using var response = await client.GetAsync($"{Program.AlunosAPI}/detalhes-aluno/{id}");
                
                if(response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var aluno = JsonConvert.DeserializeObject<Aluno>(responseBody);
                    return aluno;
                }    
            }
            catch
            {
                throw;
            }

            return null;
        }
    }
}