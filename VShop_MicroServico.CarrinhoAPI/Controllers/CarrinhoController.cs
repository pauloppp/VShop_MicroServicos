using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop_MicroServico.CarrinhoAPI.DTOs;
using VShop_MicroServico.CarrinhoAPI.Repositorios.Interfaces;

namespace VShop_MicroServico.CarrinhoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoRepositorio _repositorio;

        public CarrinhoController(ICarrinhoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet("getCarrinho/{id}")]
        public async Task<ActionResult<CarrinhoDTO>> GetByUserId(string userId)
        {
            var carrinhoDTO = await _repositorio.GetCarrinhoByUserIdAsync(userId);
            if (carrinhoDTO is null) return NotFound();
            return Ok(carrinhoDTO);
        }

        [HttpPost("addCarrinho")]
        public async Task<ActionResult<CarrinhoDTO>> AdicionarCarrinho(CarrinhoDTO carrinhoDTO)
        {
            var carrinho = await _repositorio.AtualizarCarrinhoAsync(carrinhoDTO);
            if (carrinho is null) return NotFound();
            return Ok(carrinhoDTO);
        }

        [HttpPut("updateCarrinho")]
        public async Task<ActionResult<CarrinhoDTO>> AtualizarCarrinho(CarrinhoDTO carrinhoDTO)
        {
            var carrinho = await _repositorio.AtualizarCarrinhoAsync(carrinhoDTO);
            if (carrinho is null) return NotFound();
            return Ok(carrinhoDTO);
        }

        [HttpDelete("deleteCarrinho/{id}")]
        public async Task<ActionResult<bool>> ExcluirCarrinho(int id)
        {
            var status = await _repositorio.ExcluirItemCarrinhoAsync(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

    }
}
