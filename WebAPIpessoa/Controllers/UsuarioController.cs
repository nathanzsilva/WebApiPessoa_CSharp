using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebAPIpessoa.Application.Usuario;
using WebAPIPessoa.Repository;

namespace WebAPIpessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly PessoaContext _context;
        public UsuarioController(PessoaContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult InserirUsuário([FromBody] UsuarioRequest request)
        {
            var usuarioService = new UsuarioService(_context);
            var sucesso = usuarioService.InserirUsuario(request);

            if (sucesso)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult ObterUsuarios()
        {
            var usuarioService = new UsuarioService(_context);
            var usuarios = usuarioService.ObterUsuarios();
            return Ok(usuarios);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult ObterUsuario([FromRoute] int id)
        {
            var usuarioService = new UsuarioService(_context);
            var usuario = usuarioService.ObterUsuario(id);

            if (usuario == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(usuario);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult AtualizarUsuario([FromRoute] int id, [FromBody] UsuarioRequest request)
        {
            var usuarioService = new UsuarioService(_context);
            var sucesso = usuarioService.AtualizarUsuario(id, request);

            if (sucesso)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoverUsuario([FromRoute] int id)
        {
            var usuarioService = new UsuarioService(_context);
            var usuario = usuarioService.RemoverUsuario(id);
            if (usuario)
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
