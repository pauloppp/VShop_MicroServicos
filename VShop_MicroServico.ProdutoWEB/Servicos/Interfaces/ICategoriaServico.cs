using VShop_MicroServico.ProdutoWEB.Models;

namespace VShop_MicroServico.ProdutoWEB.Servicos.Interfaces
{
    public interface ICategoriaServico
    {
        Task<IEnumerable<CategoriaViewModel>> GetAllCategorias(string token);
    }
}
