using AutoMapper;
using VShop_MicroServico.DiscontoAPI.Models;

namespace VShop_MicroServico.DiscontoAPI.DTOs.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<CouponDTO, Coupon>().ReverseMap();
        }
    }
}
