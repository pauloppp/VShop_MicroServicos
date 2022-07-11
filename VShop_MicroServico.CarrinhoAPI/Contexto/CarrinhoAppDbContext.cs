using Microsoft.EntityFrameworkCore;
using VShop_MicroServico.CarrinhoAPI.Models;

namespace VShop_MicroServico.CarrinhoAPI.Contexto
{
    public class CarrinhoAppDbContext : DbContext
    {
        public CarrinhoAppDbContext(DbContextOptions<CarrinhoAppDbContext> options) : base(options) { }
        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<CarrinhoItem> CarrinhoItems { get; set; }
        public DbSet<CarrinhoCabec> CarrinhoCabecs { get; set; }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Produto
            mb.Entity<Produto>().HasKey(p => p.Id);
            mb.Entity<Produto>().Property(p => p.Id).ValueGeneratedNever();
            mb.Entity<Produto>().Property(p => p.Nome).HasMaxLength(100).IsRequired();
            mb.Entity<Produto>().Property(p => p.Descricao).HasMaxLength(255).IsRequired();
            mb.Entity<Produto>().Property(p => p.ImageURL).HasMaxLength(255).IsRequired();
            mb.Entity<Produto>().Property(p => p.CategoryNome).HasMaxLength(100).IsRequired();
            mb.Entity<Produto>().Property(p => p.Preco).HasPrecision(12, 2);

            //CarrinhoCabec
            mb.Entity<CarrinhoCabec>().Property(cc => cc.UserId).HasMaxLength(255).IsRequired();
            mb.Entity<CarrinhoCabec>().Property(cc => cc.CouponCode).HasMaxLength(100);
        }

    }

}

