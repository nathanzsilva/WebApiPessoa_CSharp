using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using WebAPIpessoa.Application.Pessoa;
using WebAPIpessoa.Application.Usuario;
using WebAPIPessoa.Repository;

namespace WebAPIpessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        /// <summary>
        /// Rota responsável por realizar processamentos de uma pessoa.
        /// </summary>
        /// <returns>Retorna os dados da pessoa</returns>
        /// <response code="200">Retorna os dados processados da pessoa com sucesso</response>
        /// <response code="400">Erro de validação</response>

        private readonly PessoaContext _context;
        public PessoaController(PessoaContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Authorize]
        public PessoaResponse ProcessarInformacoeesPessoa([FromBody] PessoaRequest request)
        {
            var identidade = (ClaimsIdentity)HttpContext.User.Identity;
            var usuarioId = identidade.FindFirst("usuarioId").Value;
            var pessoaService = new PessoaService(_context);
            var pessoaResponse = pessoaService.ProcessarInformacoes(request, Convert.ToInt32(usuarioId));

                return pessoaResponse;
        }

        [HttpGet]
        [Authorize]
        public List<PessoaHistoricoResponse> ObterHIstoricoPessoas()
        {
            var pessoaService = new PessoaService(_context);
            var pessoas = pessoaService.ObterHistoricoPessoas();

            return pessoas;
        }
        
        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public PessoaHistoricoResponse ObterHistoricoPessoa([FromRoute] int id)
        {
            var pessoaService = new PessoaService(_context);
            var pessoa = pessoaService.ObterHistoricoPessoa(id);

            return pessoa;

        }
        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public IActionResult DeletarPessoa([FromRoute] int id)
        {
            var pessoaService = new PessoaService(_context);
            var  pessoa = pessoaService.RemoverPessoa(id);
            if (pessoa)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
