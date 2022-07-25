using VShop_MicroServico.ProdutoWEB.Models;

namespace VShop_MicroServico.ProdutoWEB.Servicos.Interfaces
{
    public interface ICarrinhoServico
    {
        Task<CarrinhoViewModel> GetCarrinhoByUserIdAsync(string userId, string token);
        Task<CarrinhoViewModel> AddItemToCarrinhoAsync(CarrinhoViewModel cartVM, string token);
        Task<CarrinhoViewModel> UpdateCarrinhoAsync(CarrinhoViewModel cartVM, string token);
        Task<bool> RemoveItemFromCarrinhoAsync(int cartId, string token);

        // Implementar futuramente
        Task<bool> ApplyCouponAsync(CarrinhoViewModel cartVM, string couponCode, string token);
        Task<bool> RemoveCouponAsync(string userId, string token);
        Task<bool> ClearCarrinhoAsync(string userId, string token);

        Task<CarrinhoViewModel> CheckoutAsync(CarrinhoCabecViewModel cartHeader, string token);
    }
}
