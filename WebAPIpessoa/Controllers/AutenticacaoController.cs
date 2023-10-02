using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPIpessoa.Application.Autenticacao;
using WebAPIpessoa.Application.Eventos;
using WebAPIPessoa.Repository;

namespace WebAPIpessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;
        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] AutenticacaoRequest request)
        {
            var tokenString = _autenticacaoService.Autenticar(request);

            if (string.IsNullOrWhiteSpace(tokenString))
            {
                return Unauthorized();
            }
            else
            {
                return Ok(new { token = tokenString });
            }
        }
        [HttpPost]
        [Route("esquecisenha")]
        public IActionResult EsqueciSenha([FromBody] EsqueciSenhaRequest request)
        {
            var resposta = _autenticacaoService.EsqueciSenha(request.email);

            if (resposta)
                return NoContent();
            else
                return BadRequest();
        }

    }
}
