using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop_MicroServico.DiscontoAPI.DTOs;
using VShop_MicroServico.DiscontoAPI.Repositorios.Interfaces;

namespace VShop_MicroServico.DiscontoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private ICouponRepositorio _repositorio;

        public CouponController(ICouponRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet("{couponCode}")]
        [Authorize]
        public async Task<ActionResult<CouponDTO>> GetDiscountCouponByCode(string couponCode)
        {
            var coupon = await _repositorio.GetCouponByCode(couponCode);

            if (coupon is null)
            {
                return NotFound($"Coupon Code: {couponCode} not found");
            }
            return Ok(coupon);
        }
    }
}
