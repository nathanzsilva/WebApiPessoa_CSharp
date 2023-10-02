using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPIpessoa.Application.Pessoa
{
    public class PessoaHistoricoResponse : PessoaRequest
    {
        public int Idade { get; set; }

        public Decimal IMC { get; set; }

        public String Classificacao { get; set; }

        public Double INSS { get; set; }

        public Double Aliquota { get; set; }

        public Double SalarioLiquido { get; set; }

        public Decimal SaldoDolar { get; set; }
        public int idUsuario { get; set; }
        public int Id { get; set; }
    }
}
