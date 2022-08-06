using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop_MicroServico.ProdutoWEB.Models;
using VShop_MicroServico.ProdutoWEB.Servicos.Interfaces;

namespace VShop_MicroServico.ProdutoWEB.Controllers
{
    public class CarrinhoController : Controller
    {

        private readonly ICarrinhoServico _carrinhoServico;

        public CarrinhoController(ICarrinhoServico carrinhoServico)
        {
            _carrinhoServico = carrinhoServico;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            CarrinhoViewModel? carrinhoVM = await GetCarrinhoByUser();

            if (carrinhoVM is null)
            {
                ModelState.AddModelError("CarrinhoNotFound", "Does not exist a cart yet...Come on Shopping...");
                return View("/Views/Carrinho/CarrinhoNotFound.cshtml");
            }

            return View(carrinhoVM);
        }

        private async Task<CarrinhoViewModel?> GetCarrinhoByUser()
        {

            var carrinho = await _carrinhoServico.GetCarrinhoByUserIdAsync(GetUserId(), await GetAccessToken());

            if (carrinho?.CarrinhoCabec is not null)
            {
                foreach (var item in carrinho.CarrinhoItems)
                {
                    carrinho.CarrinhoCabec.TotalAmount += (item.Produto.Preco * item.Quantity);
                }
            }
            return carrinho;
        }

        public async Task<IActionResult> RemoveItem(int id)
        {
            var result = await _carrinhoServico.RemoveItemFromCarrinhoAsync(id, await GetAccessToken());

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(id);
        }

        private async Task<string> GetAccessToken()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }

        private string GetUserId()
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            return userId;
        }
    }
}
