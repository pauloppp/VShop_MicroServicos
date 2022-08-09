using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VShop_MicroServico.DiscontoAPI.Contexto;
using VShop_MicroServico.DiscontoAPI.DTOs;
using VShop_MicroServico.DiscontoAPI.Repositorios.Interfaces;

namespace VShop_MicroServico.DiscontoAPI.Repositorios.Concretas
{
    public class CouponRepositorio : ICouponRepositorio
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;

        public CouponRepositorio(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponDTO> GetCouponByCode(string couponCode)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
            return _mapper.Map<CouponDTO>(coupon);
        }
    }
}
