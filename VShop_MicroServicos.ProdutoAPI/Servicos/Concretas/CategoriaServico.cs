using AutoMapper;
using VShop_MicroServicos.ProdutoAPI.DTOs;
using VShop_MicroServicos.ProdutoAPI.Models;
using VShop_MicroServicos.ProdutoAPI.Repositorios.Interfaces;
using VShop_MicroServicos.ProdutoAPI.Servicos.Interfaces;

namespace VShop_MicroServicos.ProdutoAPI.Servicos.Concretas
{
    public class CategoriaServico : ICategoriaServico
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly IMapper _mapper;

        // Injeção de dependência pelo construtor
        public CategoriaServico(ICategoriaRepositorio categoriaRepositorio, IMapper mapper)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
        {
            var categoriasEntity = await _categoriaRepositorio.GetAll();
            return _mapper.Map<IEnumerable<CategoriaDTO>>(categoriasEntity);
        }

        public async Task<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
        {
            var categoriasEntity = await _categoriaRepositorio.GetCategoriasProdutos();
            return _mapper.Map<IEnumerable<CategoriaDTO>>(categoriasEntity);
        }

        public async Task<CategoriaDTO> GetCategoriaById(int id)
        {
            var categoriasEntity = await _categoriaRepositorio.GetById(id);
            return _mapper.Map<CategoriaDTO>(categoriasEntity);
        }

        public async Task<CategoriaDTO> AddCategoria(CategoriaDTO categoriaDTO)
        {
            var categoriasEntity = _mapper.Map<Categoria>(categoriaDTO);
            await _categoriaRepositorio.Create(categoriasEntity);
            categoriaDTO.Id = categoriasEntity.Id;
            return categoriaDTO;
        }

        public async Task<CategoriaDTO> UpdateCategoria(CategoriaDTO categoriaDTO)
        {
            var categoriasEntity = _mapper.Map<Categoria>(categoriaDTO);
            await _categoriaRepositorio.Update(categoriasEntity);
            return categoriaDTO;
        }

        public async Task<CategoriaDTO> RemoveCategoria(int id)
        {
            var categoriasEntity = _categoriaRepositorio.GetById(id).Result;
            await _categoriaRepositorio.Delete(categoriasEntity.Id);
            return _mapper.Map<CategoriaDTO>(categoriasEntity);
        }
    }
}
