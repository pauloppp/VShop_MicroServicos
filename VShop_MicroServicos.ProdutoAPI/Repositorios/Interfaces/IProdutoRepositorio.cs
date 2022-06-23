using VShop_MicroServicos.ProdutoAPI.Models;

namespace VShop_MicroServicos.ProdutoAPI.Repositorios.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(int id);
        Task<Produto> Create(Produto produto);
        Task<Produto> Update(Produto produto);
        Task<Produto> Delete(int id);
    }
}
