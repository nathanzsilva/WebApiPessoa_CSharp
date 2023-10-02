using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebAPIpessoa.Application.Eventos;
using WebAPIpessoa.Application.Eventos.Models;
using WebAPIPessoa.Repository;
using WebAPIPessoa.Repository.Models;

namespace WebAPIpessoa.Application.Autenticacao
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly PessoaContext _context;
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public AutenticacaoService(PessoaContext context, IRabbitMQProducer rabbitMQProducer)
        {
            _context = context;
            _rabbitMQProducer = rabbitMQProducer;
        }
        public bool EsqueciSenha(string email)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(x => x.email == email);
                if (usuario == null)
                    return false;

                var esqueciSenha = new EsqueciSenhaModel()
                {
                    Email = email,
                    Assunto = "Recuperação de Senha",
                    Texto = $"Sua Senha é {usuario.senha}"
                };
                

                _rabbitMQProducer.EnviarMensagem(esqueciSenha, "Var.Notificacao.Email", "Var.Notificacao", "Var.Notificacao");

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public string Autenticar(AutenticacaoRequest request)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.usuario == request.UserName && x.senha == request.Password);
            if (usuario != null)
            {
                var tokenString = GeraTokenJwt(usuario);
                return tokenString;
            }
            else
            {
                return null;
            }
        }
        partial class loginrequest
        {
            public int id { get; set; }
        ´public int nivelacesso { get;set }
        }
        private string GeraTokenJwt(loginrequest usuario)
        {
            var issuer = "var";
            var audience = "var";
            var key = "c013239a-5e89-4749-b0bb-07fe4d21710d";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("usuarioID", usuario.nivelacesso.ToString())
            };

            var token = new JwtSecurityToken(issuer: issuer, claims: claims, audience: audience, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;

        }
    }
}
