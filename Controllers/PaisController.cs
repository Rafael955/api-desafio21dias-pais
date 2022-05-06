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
        [HttpGet("detalhes-do-pai/{id:int?}")]
        public async Task<IActionResult> Details(ObjectId id)
        {
            if (id == null)
            {
                return StatusCode(404, new { Mensagem = "Pai não foi encontrado!"});
            }

            var pai = await paiMongoRepository.BuscarPorId(id);

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
                
               
                return StatusCode(201, pai);
            }

            return StatusCode(400, new { Mensagem = "O pai passado é inválido!" });
        }

        // POST: atualizar-dados-pai/5
        [HttpPut("atualizar-dados-pai/{id:int}")]
        public async Task<IActionResult> Edit(ObjectId id, Pai pai)
        {
            if(!(await AlunoServico.ValidarUsuario(pai.AlunoId)))
                    return StatusCode(400, new { Mensagem = $"Usuário de ID {pai.AlunoId} não é válido ou não existe!"});

            try
            {
                pai.Id = id;
                paiMongoRepository.Salvar(pai);
            }
            catch (Exception error)
            {
                if (!await PaiExists(pai.Id))
                {
                    return StatusCode(404, new { Mensagem = "Pai para atualizar não foi encontrado!"});
                }
                else
                {
                    throw error;
                }
            }
            
            return StatusCode(200, pai);
        }

        // POST: remover-pai/5
        [HttpDelete("remover-pai/{id:int}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            var pai = await paiMongoRepository.BuscarPorId(id);
            
            if(pai == null)
                return StatusCode(404, new { Mensagem = "Pai não encontrado!"});

            paiMongoRepository.RemoverPorId(id);
            return StatusCode(204);
        }

        private async Task<bool> PaiExists(ObjectId id)
        {
            return await paiMongoRepository.BuscarPorId(id) != null ? true : false;
        }
    }
}
