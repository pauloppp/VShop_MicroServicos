using VShop_MicroServico.ProdutoWEB.Models;

namespace VShop_MicroServico.ProdutoWEB.Servicos.Interfaces
{
    public interface IProdutoServico
    {
        Task<IEnumerable<ProdutoViewModel>> GetAllProdutos(string token);
        Task<ProdutoViewModel> FindProdutoById(int id, string token);
        Task<ProdutoViewModel> CreateProduto(ProdutoViewModel produtoViewModel, string token);
        Task<ProdutoViewModel> UpdateProduto(ProdutoViewModel produtoViewModel, string token);
        Task<bool> DeleteProdutoById(int id, string token);
    }
}
