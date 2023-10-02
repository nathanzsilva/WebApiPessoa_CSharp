using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAPIPessoa.Repository;
using WebAPIPessoa.Repository.Models;

namespace WebAPIpessoa.Application.Usuario
{
    public class UsuarioService
    {
        private readonly PessoaContext _context;
        public UsuarioService(PessoaContext context)
        {
            _context = context;
        }
        public bool InserirUsuario(UsuarioRequest request)
        {
            try
            {
                var usuario = new TabUsuario()
                {
                    nome = request.Nome,
                    usuario = request.Usuario,
                    senha = request.Senha,
                    email = request.Email
                };

                if (_context.Usuarios.Any(x => x.email == request.Email))
                    return false;

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<TabUsuario> ObterUsuarios()
        {
            var usuarios = _context.Usuarios.ToList();

            return usuarios;
        }
        public TabUsuario ObterUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.id == id);
            return usuario;
        }
        public bool AtualizarUsuario(int id, UsuarioRequest request)
        {
            try
            {
                var usuarioDb = _context.Usuarios.FirstOrDefault(x => x.id == id);
                if(usuarioDb is null)
                {
                    return false;
                }
                usuarioDb.nome = request.Nome; 
                usuarioDb.senha= request.Senha; 
                usuarioDb.usuario = request.Usuario;
                usuarioDb.email = request.Email;

                _context.Usuarios.Update(usuarioDb);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool RemoverUsuario(int id)
        {
            try
            {
                var usuarioDb = _context.Usuarios.FirstOrDefault(x => x.id == id);
                if (usuarioDb == null)
                {
                    return false;
                }
                _context.Usuarios.Remove(usuarioDb);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
