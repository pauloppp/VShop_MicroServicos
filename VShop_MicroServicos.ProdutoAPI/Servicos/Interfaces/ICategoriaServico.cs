using VShop_MicroServicos.ProdutoAPI.DTOs;

namespace VShop_MicroServicos.ProdutoAPI.Servicos.Interfaces
{
    public interface ICategoriaServico
    {
        // O padrão DTO é similar ao padrão ViewModel
        Task<IEnumerable<CategoriaDTO>> GetCategorias();
        Task<IEnumerable<CategoriaDTO>> GetCategoriasProdutos();
        Task<CategoriaDTO> GetCategoriaById(int id);
        Task<CategoriaDTO> AddCategoria(CategoriaDTO categoriaDTO);
        Task<CategoriaDTO> UpdateCategoria(CategoriaDTO categoriaDTO);
        Task<CategoriaDTO> RemoveCategoria(int id);
    }
}
