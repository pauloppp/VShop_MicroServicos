using AutoMapper;
using VShop_MicroServico.CarrinhoAPI.Models;

namespace VShop_MicroServico.CarrinhoAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarrinhoDTO, Carrinho>().ReverseMap();
            CreateMap<CarrinhoCabecDTO, CarrinhoCabec>().ReverseMap();
            CreateMap<CarrinhoItemDTO, CarrinhoItem>().ReverseMap();
            CreateMap<ProdutoDTO, Produto>().ReverseMap();
        }
    }
}
