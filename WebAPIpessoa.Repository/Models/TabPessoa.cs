using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCacaTesouro.Repository.Models
{
    public class TabPessoa
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
        public decimal Salario { get; set; }
        public int Idade { get; set; }
        public decimal IMC { get; set; }
        public string Classificacao { get; set; }
        public decimal INSS { get; set; }
        public decimal Aliquota { get; set; }
        public decimal SalarioLiquido { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoDolar { get; set; }
        public int idUsuario { get; set; }
    }
}
