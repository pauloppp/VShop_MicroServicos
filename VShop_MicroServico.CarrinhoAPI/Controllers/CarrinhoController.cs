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

        [HttpGet("getcart/{userid}")]
        public async Task<ActionResult<CarrinhoDTO>> GetByUserId(string userid)
        {
            var carrinhoDTO = await _repositorio.GetCarrinhoByUserIdAsync(userid);
            if (carrinhoDTO is null) return NotFound();
            return Ok(carrinhoDTO);
        }

        [HttpPost("addcarrinho")]
        public async Task<ActionResult<CarrinhoDTO>> AdicionarCarrinho(CarrinhoDTO carrinhoDTO)
        {
            var carrinho = await _repositorio.AtualizarCarrinhoAsync(carrinhoDTO);
            if (carrinho is null) return NotFound();
            return Ok(carrinhoDTO);
        }

        [HttpPut("updatecarrinho")]
        public async Task<ActionResult<CarrinhoDTO>> AtualizarCarrinho(CarrinhoDTO carrinhoDTO)
        {
            var carrinho = await _repositorio.AtualizarCarrinhoAsync(carrinhoDTO);
            if (carrinho is null) return NotFound();
            return Ok(carrinhoDTO);
        }

        [HttpDelete("deletecarrinho/{id}")]
        public async Task<ActionResult<bool>> ExcluirCarrinho(int id)
        {
            var status = await _repositorio.ExcluirItemCarrinhoAsync(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

    }
}
