using VShop_MicroServico.ProdutoWEB.Models;

namespace VShop_MicroServico.ProdutoWEB.Servicos.Interfaces
{
    public interface IProdutoServico
    {
        Task<IEnumerable<ProdutoViewModel>> GetAllProdutos();
        Task<ProdutoViewModel> FindProdutoById(int id);
        Task<ProdutoViewModel> CreateProduto(ProdutoViewModel produtoViewModel);
        Task<ProdutoViewModel> UpdateProduto(ProdutoViewModel produtoViewModel);    
        Task <bool>DeleteProdutoById(int id);
    }
}
