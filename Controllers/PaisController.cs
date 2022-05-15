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
        public async Task<IActionResult> Index(int page = 1)
        {
            return StatusCode(200, await paiMongoRepository.Todos());
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
                if(!(await AlunoServico.ValidarUsuario(pai.AlunoId)))
                    return StatusCode(400, new { Mensagem = $"Usuário aluno de ID {pai.AlunoId} não é válido ou não existe!"});

                try
                {
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
            if(!(await AlunoServico.ValidarUsuario(pai.AlunoId)))
                    return StatusCode(400, new { Mensagem = $"Usuário de ID {pai.AlunoId} não é válido ou não existe!"});

            try
            {
                pai.Id = ObjectId.Parse(id);
                paiMongoRepository.Atualizar(pai);
            }
            catch
            {
                return StatusCode(404, new { Mensagem = "Pai para atualizar não foi encontrado!"});
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
