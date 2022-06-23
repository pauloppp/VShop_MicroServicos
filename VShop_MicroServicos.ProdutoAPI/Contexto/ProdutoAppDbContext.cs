using Microsoft.EntityFrameworkCore;
using VShop_MicroServicos.ProdutoAPI.Models;

namespace VShop_MicroServicos.ProdutoAPI.Contexto
{
    public class ProdutoAppDbContext : DbContext
    {
        public ProdutoAppDbContext(DbContextOptions<ProdutoAppDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        //Fluent_API
        protected override void OnModelCreating(ModelBuilder mb)
        {

            //Categoria
            mb.Entity<Categoria>().HasKey(c => c.Id);
            mb.Entity<Categoria>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
            mb.Entity<Categoria>().HasMany(p => p.Produtos).WithOne(c => c.Categoria).IsRequired().OnDelete(DeleteBehavior.Cascade);
            mb.Entity<Categoria>().HasData(new Categoria { Id = 1, Nome = "Material Escolar", }, new Categoria { Id = 2, Nome = "Acessórios", });


            //Produto
            mb.Entity<Produto>().Property(c => c.Nome).HasMaxLength(100).IsRequired();
            mb.Entity<Produto>().Property(c => c.Descricao).HasMaxLength(255).IsRequired();
            mb.Entity<Produto>().Property(c => c.ImagemURL).HasMaxLength(255).IsRequired();
            mb.Entity<Produto>().Property(c => c.Preco).HasPrecision(18, 2);


        }

    }
}
