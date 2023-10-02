using System;
using System.Globalization;

namespace WebAPIpessoa
{
    public class PessoaResponse
    {
        public string Nome { get; set; }

        public int Idade { get; set; }  

        public Decimal IMC { get; set; }    

        public String Classificacao { get; set; }

        public Double INSS { get; set; }

        public Double Aliquota { get; set; }

        public Double SalarioLiquido { get; set; }

        public Decimal SaldoDolar { get; set; }
    }
}
