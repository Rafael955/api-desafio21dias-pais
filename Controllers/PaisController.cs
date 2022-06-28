using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using webapi.Models;
using webapi_materiais.Servico;
using webapi_pais.Servico;

namespace webapi.Controllers
{
    [ApiController]
    [Route("pais/api")]
    public class PaisController : Controller
    {
        private PaisMongodb paiMongoRepository;
        public PaisController()
        {
            paiMongoRepository = new PaisMongodb();
        }
        // GET: listar-pais-de-alunos
        [HttpGet("listar-pais-de-alunos")]
        public async Task<IActionResult> Index()
        {
            var todos = await paiMongoRepository.Todos();
            return Ok(todos);
        }

        // GET: detalhes-pai/5
        [HttpGet("detalhes-do-pai/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return StatusCode(404, new { Mensagem = "Pai não foi encontrado!"});
            }

            var pai = await paiMongoRepository.BuscarPorId(ObjectId.Parse(id));

            if (pai == null)
            {
                return StatusCode(404, new { Mensagem = "Pai não foi encontrado!"});
            }

            return StatusCode(200, pai);
        }

        // POST: cadastrar-pai
        [HttpPost("cadastrar-pai")]
        public async Task<IActionResult> Create(Pai pai)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var statusCode = await AlunoServico.ValidarUsuario(pai.AlunoId);

                    if(statusCode == System.Net.HttpStatusCode.NotFound)
                        return StatusCode(404, new { Mensagem = $"Usuário aluno de ID {pai.AlunoId} não é válido ou não existe!"});
                    
                    if(statusCode == System.Net.HttpStatusCode.InternalServerError)
                        return StatusCode(500, new { Mensagem = $"Erro ao consultar dados do aluno ou serviço indisponível!"});

                    pai.Aluno = await AlunoServico.BuscarPorId(pai.AlunoId);
                
                    paiMongoRepository.Inserir(pai);
                }
                catch
                {
                    return StatusCode(404, new { Mensagem = "Houve um erro ao cadastrar o Pai!"});
                }
               
                return StatusCode(201, pai);
            }

             return StatusCode(404, new { Mensagem = "Houve um erro ao cadastrar o Pai!"});
        }

        // POST: atualizar-dados-pai/5
        [HttpPut("atualizar-dados-pai/{id}")]
        public async Task<IActionResult> Edit(string id, Pai pai)
        {
            try
            {
                var statusCode = await AlunoServico.ValidarUsuario(pai.AlunoId);

                if(statusCode == System.Net.HttpStatusCode.NotFound)
                    return StatusCode(404, new { Mensagem = $"Usuário aluno de ID {pai.AlunoId} não é válido ou não existe!"});
                
                if(statusCode == System.Net.HttpStatusCode.InternalServerError)
                    return StatusCode(500, new { Mensagem = $"Erro ao consultar dados do aluno ou serviço indisponível!"});

                pai.Aluno = await AlunoServico.BuscarPorId(pai.AlunoId);
                pai.Id = id;
                
                paiMongoRepository.Atualizar(pai);
            }
            catch(Exception ex)
            {
                if(!(await PaiExists(ObjectId.Parse(id))))
                {
                    return StatusCode(404, new { Mensagem = "Pai para atualizar não foi encontrado!"});
                }
                else
                {
                     return StatusCode(500, new { Mensagem = ex.Message});
                }
            }
            
            return StatusCode(200, pai);
        }

        // POST: remover-pai/5
        [HttpDelete("remover-pai/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var pai = await paiMongoRepository.BuscarPorId(ObjectId.Parse(id));
            
            if(pai == null)
                return StatusCode(404, new { Mensagem = "Pai não encontrado!"});

            paiMongoRepository.RemoverPorId(ObjectId.Parse(id));
            return StatusCode(204);
        }

        private async Task<bool> PaiExists(ObjectId id)
        {
            return await paiMongoRepository.BuscarPorId(id) != null ? true : false;
        }
    }
}
