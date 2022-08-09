using Microsoft.EntityFrameworkCore;
using VShop_MicroServico.DiscontoAPI.Models;

namespace VShop_MicroServico.DiscontoAPI.Contexto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }

        // FluentAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "VSHOP_PROMO_10",
                Discount = 10
            });
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "VSHOP_PROMO_20",
                Discount = 20
            });
        }
    }
}
