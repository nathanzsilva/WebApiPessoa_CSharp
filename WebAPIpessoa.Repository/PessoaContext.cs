using ApiCacaTesouro.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPIPessoa.Repository.Models;

namespace WebAPIPessoa.Repository
{
    public class PessoaContext: DbContext
    {
        public PessoaContext(DbContextOptions<PessoaContext> option) : base(option) { }
        public DbSet<TabPessoa> Pessoas { get; set; } 
        public DbSet<TabUsuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TabPessoa>().ToTable("TabPessoa");
            modelBuilder.Entity<TabUsuario>().ToTable("TabUsuario");
        }
    }
}
