using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPIpessoa.Application.Autenticacao
{
    public interface IAutenticacaoService
    {
        bool EsqueciSenha(string email);
        string Autenticar(AutenticacaoRequest request);
    }
}
