using System;

namespace WebAPIpessoa
{
    public class PessoaRequest
    {
        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public Decimal Altura { get; set; }

        public Decimal Peso { get; set; }   

        public Double Salario { get; set; }

        public Decimal Saldo { get; set; }
    }
}
