using AutoMapper;
using VShop_MicroServicos.ProdutoAPI.DTOs;
using VShop_MicroServicos.ProdutoAPI.Models;
using VShop_MicroServicos.ProdutoAPI.Repositorios.Interfaces;
using VShop_MicroServicos.ProdutoAPI.Servicos.Interfaces;

namespace VShop_MicroServicos.ProdutoAPI.Servicos.Concretas
{
    public class ProdutoServico : IProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IMapper _mapper;

        public ProdutoServico(IProdutoRepositorio produtoRepositorio, IMapper mapper)
        {
            _produtoRepositorio = produtoRepositorio;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
        {
            var produtosEntity = await _produtoRepositorio.GetAll();
            return _mapper.Map<IEnumerable<ProdutoDTO>>(produtosEntity);
        }

        public async Task<ProdutoDTO> GetProdutosById(int id)
        {
            var produtosEntity = await _produtoRepositorio.GetById(id);
            return _mapper.Map<ProdutoDTO>(produtosEntity);
        }

        public async Task<ProdutoDTO> AddProduto(ProdutoDTO produtoDTO)
        {
            var produtosEntity = _mapper.Map<Produto>(produtoDTO);
            await _produtoRepositorio.Create(produtosEntity);
            produtoDTO.Id = produtosEntity.Id;
            return produtoDTO;
        }

        public async Task<ProdutoDTO> UpdateProduto(ProdutoDTO produtoDTO)
        {
            var produtosEntity = _mapper.Map<Produto>(produtoDTO);
            await _produtoRepositorio.Update(produtosEntity);
            return produtoDTO;
        }

        public async Task<ProdutoDTO> RemoveProduto(int id)
        {
            var produtosEntity = _produtoRepositorio.GetById(id).Result;
            await _produtoRepositorio.Delete(produtosEntity.Id);
            return _mapper.Map<ProdutoDTO>(produtosEntity);
        }
    }
}
