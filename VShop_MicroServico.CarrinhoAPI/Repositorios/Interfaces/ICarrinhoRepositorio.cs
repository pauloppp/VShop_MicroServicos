using VShop_MicroServico.CarrinhoAPI.DTOs;

namespace VShop_MicroServico.CarrinhoAPI.Repositorios.Interfaces
{
    public interface ICarrinhoRepositorio
    {
        Task<CarrinhoDTO> GetCarrinhoByUserIdAsync(string userId);
        Task<CarrinhoDTO> AtualizarCarrinhoAsync(CarrinhoDTO carrinho);
        Task<bool> LimparCarrinhoAsync(string userId);
        Task<bool> ExcluirItemCarrinhoAsync(int carrinhoItemId);
        Task<bool> AplicarCouponAsync(string userId, string couponCode);   
        Task<bool> ExcluirCouponAsync(string userId);
    }
}
