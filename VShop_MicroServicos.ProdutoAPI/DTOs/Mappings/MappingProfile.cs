using AutoMapper;
using VShop_MicroServicos.ProdutoAPI.Models;

namespace VShop_MicroServicos.ProdutoAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProdutoDTO, Produto>();
            CreateMap<Produto, ProdutoDTO>().ForMember(x => x.CategoriaNome, opt => opt.MapFrom(src => src.Categoria.Nome));
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        }
    }
}
