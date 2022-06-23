using Microsoft.EntityFrameworkCore;
using VShop_MicroServicos.ProdutoAPI.Contexto;
using VShop_MicroServicos.ProdutoAPI.Models;
using VShop_MicroServicos.ProdutoAPI.Repositorios.Interfaces;

namespace VShop_MicroServicos.ProdutoAPI.Repositorios.Concretas
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly ProdutoAppDbContext _produtoAppDbContext;

        public CategoriaRepositorio(ProdutoAppDbContext produtoAppDbContext)
        {
            _produtoAppDbContext = produtoAppDbContext;
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _produtoAppDbContext.Categorias.ToListAsync();
        }

        public async Task<Categoria> GetById(int id)
        {
            return await _produtoAppDbContext.Categorias.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return await _produtoAppDbContext.Categorias.Include(p => p.Produtos).ToListAsync();
        }

        public async Task<Categoria> Create(Categoria categoria)
        {
            _produtoAppDbContext.Categorias.Add(categoria);
            await _produtoAppDbContext.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> Update(Categoria categoria)
        {
            _produtoAppDbContext.Entry(categoria).State = EntityState.Modified;
            await _produtoAppDbContext.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> Delete(int id)
        {
            var categoria = await GetById(id);
            _produtoAppDbContext.Remove(categoria);
            return categoria;
        }

    }
}
