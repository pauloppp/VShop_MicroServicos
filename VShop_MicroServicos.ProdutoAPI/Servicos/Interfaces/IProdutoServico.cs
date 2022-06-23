using VShop_MicroServicos.ProdutoAPI.DTOs;

namespace VShop_MicroServicos.ProdutoAPI.Servicos.Interfaces
{
    public interface IProdutoServico
    {
        // O padrão DTO é similar ao padrão ViewModel
        Task<IEnumerable<ProdutoDTO>> GetProdutos();
        Task<ProdutoDTO> GetProdutosById(int id);
        Task<ProdutoDTO> AddProduto(ProdutoDTO produto);
        Task<ProdutoDTO> UpdateProduto(ProdutoDTO produto);
        Task<ProdutoDTO> RemoveProduto(int id);
    }
}
