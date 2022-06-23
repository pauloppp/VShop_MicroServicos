using Microsoft.EntityFrameworkCore;
using VShop_MicroServicos.ProdutoAPI.Contexto;
using VShop_MicroServicos.ProdutoAPI.Models;
using VShop_MicroServicos.ProdutoAPI.Repositorios.Interfaces;

namespace VShop_MicroServicos.ProdutoAPI.Repositorios.Concretas
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly ProdutoAppDbContext _produtoAppDbContext;

        public ProdutoRepositorio(ProdutoAppDbContext produtoAppDbContext)
        {
            _produtoAppDbContext = produtoAppDbContext;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _produtoAppDbContext.Produtos.Include(c => c.Categoria).ToListAsync();
        }

        public async Task<Produto> GetById(int id)
        {
            return await _produtoAppDbContext.Produtos.Include(c => c.Categoria).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Produto> Create(Produto produto)
        {
            _produtoAppDbContext.Add(produto);
            await _produtoAppDbContext.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> Update(Produto produto)
        {
            _produtoAppDbContext.Entry(produto).State = EntityState.Modified;
            await _produtoAppDbContext.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> Delete(int id)
        {
            var produto =  await GetById(id);
            _produtoAppDbContext.Produtos.Remove(produto);
            await _produtoAppDbContext.SaveChangesAsync();
            return produto;
        }

    }
}
