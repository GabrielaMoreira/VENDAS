using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Models;
using sistema_vendas_web.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sistema_vendas_web.Data
{
    public class VendasContext : DbContext
    {
        public VendasContext(DbContextOptions<VendasContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Fisico> Fisicos { get; set; }
        public DbSet<Juridico> Juridicos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }
        public DbSet<NotaFiscal> NotaFiscais { get; set; }
        public DbSet<ItemNota> ItemNotas { get; set; }



        /*EXEÇÕES: DADOS UNICOS EM BASE DE DADOS*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(usuario => new { usuario.CPF })
                .IsUnique(true);

            modelBuilder.Entity<Usuario>()
                .HasIndex(usuario => new { usuario.LOGIN })
                .IsUnique(true);
        }
    }
}
