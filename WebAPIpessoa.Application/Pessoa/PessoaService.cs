using ApiCacaTesouro.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAPIPessoa.Repository;
using WebAPIPessoa.Repository.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPIpessoa.Application.Pessoa
{
    public class PessoaService
    {
        private readonly PessoaContext _context;
        public PessoaService(PessoaContext context)
        {
            _context = context;
        }

        public bool RemoverPessoa(int id)
        {
            try
            {
                var pessoaDb = _context.Pessoas.FirstOrDefault(x => x.id == id);
                if (pessoaDb == null)
                {
                    return false;
                }
                _context.Pessoas.Remove(pessoaDb);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PessoaHistoricoResponse ObterHistoricoPessoa(int id)
        {
            var pessoaDb = _context.Pessoas.FirstOrDefault(x => x.id == id);
            var pessoa = new PessoaHistoricoResponse()
            {
                Aliquota = Convert.ToDouble(pessoaDb.Aliquota),
                Altura = pessoaDb.Altura,
                Classificacao = pessoaDb.Classificacao,
                DataNascimento = pessoaDb.DataNascimento,
                Id = pessoaDb.id,
                Idade = pessoaDb.Idade,
                idUsuario = pessoaDb.idUsuario,
                IMC = pessoaDb.IMC,
                INSS = Convert.ToDouble(pessoaDb.INSS),
                Nome = pessoaDb.Nome,
                Peso = pessoaDb.Peso,
                Salario = Convert.ToDouble(pessoaDb.Salario),
                SalarioLiquido = Convert.ToDouble(pessoaDb.SalarioLiquido),
                Saldo = pessoaDb.Saldo,
                SaldoDolar = pessoaDb.SaldoDolar
            };

            return pessoa;
        }

        public List<PessoaHistoricoResponse> ObterHistoricoPessoas()
        {
            var pessoaDB = _context.Pessoas.ToList();
            var pessoas = new List<PessoaHistoricoResponse>();

            foreach (var item in pessoaDB)
            {
                pessoas.Add(new PessoaHistoricoResponse()
                {
                    Aliquota = Convert.ToDouble(item.Aliquota),
                    Altura = item.Altura,
                    Classificacao = item.Classificacao,
                    DataNascimento = item.DataNascimento,
                    Id = item.id,
                    Idade = item.Idade,
                    idUsuario = item.idUsuario,
                    IMC = item.IMC,
                    INSS = Convert.ToDouble(item.INSS),
                    Nome = item.Nome,
                    Peso = item.Peso,
                    Salario = Convert.ToDouble(item.Salario),
                    SalarioLiquido = Convert.ToDouble(item.SalarioLiquido),
                    Saldo = item.Saldo,
                    SaldoDolar = item.SaldoDolar
                });
            }
            return pessoas;
        }
        public PessoaResponse ProcessarInformacoes(PessoaRequest request, int usuarioId)
        {
            var idade = CalcularIdade(request.DataNascimento);
            var IMC = CalcularImc(request.Peso, request.Altura);
            var classificacao = CalcularClassificacao(IMC);
            var aliquota = CalcularAliquota(request.Salario);
            var inss = CalcularINSS(request.Salario, aliquota);
            var salarioliquido = CalcularSalarioLiquido(request.Salario, inss);
            var saldoDolar = CalcularDolar(request.Saldo);


            var resposta = new PessoaResponse();

            resposta.SaldoDolar = saldoDolar;
            resposta.SalarioLiquido = salarioliquido;
            resposta.Aliquota = aliquota;
            resposta.INSS = inss;
            resposta.Classificacao = classificacao;
            resposta.Idade = idade;
            resposta.Nome = request.Nome;
            resposta.IMC = IMC;

            var pessoa = new TabPessoa()
            {
                Aliquota = Convert.ToDecimal(aliquota),
                Altura = request.Altura,
                Classificacao = classificacao,
                DataNascimento = request.DataNascimento,
                Idade = idade,
                idUsuario = usuarioId,
                IMC = IMC,
                INSS = Convert.ToDecimal(inss),
                Nome = request.Nome,
                Peso = request.Peso,
                Salario = Convert.ToDecimal(request.Salario),
                SalarioLiquido = Convert.ToDecimal(salarioliquido),
                Saldo = request.Saldo,
                SaldoDolar = saldoDolar
            };

            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();

            return resposta;
        }

        private int CalcularIdade(DateTime dataNascimento)
        {
            var anoAtual = DateTime.Now.Year;
            var mesAtual = DateTime.Now.Month;
            var diaAtual = DateTime.Now.Day;

            var idade = anoAtual - dataNascimento.Year;

            if (dataNascimento.Month == mesAtual)
            {
                if (dataNascimento.Day > diaAtual)
                {
                    idade = idade - 1;
                }
            }
            else if (dataNascimento.Month > mesAtual)
            {
                idade = idade - 1;
            }
            return idade;
        }
        private decimal CalcularImc(decimal peso, decimal altura)
        {
            var imc = Math.Round(peso / (altura * altura), 2);
            return imc;
        }
        private string CalcularClassificacao(decimal IMC)
        {
            var classificacao = "";
            if (IMC < (decimal)18.5)
            {
                classificacao = "Abaixo do peso";
            }
            else if (IMC >= (decimal)18.5 && IMC <= (decimal)24.9)
            {
                classificacao = "Normal";
            }
            else if (IMC >= (decimal)25.0 && IMC <= (decimal)29.9)
            {
                classificacao = "Sobrepeso";
            }
            else if (IMC >= (decimal)30.0 && IMC <= (decimal)34.9)
            {
                classificacao = "Obesidade grau I";
            }
            else if (IMC >= (decimal)35.0 && IMC <= (decimal)39.9)
            {
                classificacao = "Obesidade grau II";
            }
            else if (IMC >= (decimal)40.0)
            {
                classificacao = "Obesidade grau III";
            }
            return classificacao;
        }
        private double CalcularAliquota(double salario)
        {
            double aliquota = 0;
            if (salario <= 1212)
            {
                aliquota = 7.5;
            }
            else if (salario >= 1212.01 && salario <= 2427.35)
            {
                aliquota = 9;
            }
            else if (salario >= 2427.36 && salario <= 3641.03)
            {
                aliquota = 12;
            }
            else
            {
                aliquota = 14;
            }
            return aliquota;
        }
        private double CalcularINSS(double salario, double aliquota)
        {
            var inss = (salario * aliquota) / 100;
            return inss;
        }
        private double CalcularSalarioLiquido(double salario, double inss)
        {
            return salario - inss;
        }
        private decimal CalcularDolar(decimal saldo)
        {
            var dolar = (decimal)5.14;
            var saldoDolar = Math.Round(saldo / dolar, 2);

            return saldoDolar;
        }
    }
}
