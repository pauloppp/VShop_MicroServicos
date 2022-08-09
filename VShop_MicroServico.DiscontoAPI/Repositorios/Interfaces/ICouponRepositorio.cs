using VShop_MicroServico.DiscontoAPI.DTOs;

namespace VShop_MicroServico.DiscontoAPI.Repositorios.Interfaces
{
    public interface ICouponRepositorio
    {
        Task<CouponDTO> GetCouponByCode(string couponCode);
    }
}
