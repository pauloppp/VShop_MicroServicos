using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VShop_MicroServico.ProdutoWEB.Models;
using VShop_MicroServico.ProdutoWEB.Servicos.Interfaces;

namespace VShop_MicroServico.ProdutoWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdutoServico _produtoServico;
        private readonly ICarrinhoServico _carrinhoServico;
        public HomeController(ILogger<HomeController> logger, IProdutoServico produtoServico, ICarrinhoServico carrinhoServico)
        {
            _logger = logger;
            _produtoServico = produtoServico;
            _carrinhoServico = carrinhoServico;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _produtoServico.GetAllProdutos(string.Empty);

            if (products is null)
                return View("Error");

            return View(products);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProdutoViewModel>> ProductDetails(int id)
        {
            var product = await _produtoServico.FindProdutoById(id, string.Empty);

            if (product is null)
                return View("Error");

            return View(product);
        }

        [HttpPost]
        [ActionName("ProductDetails")]
        [Authorize]
        public async Task<ActionResult<ProdutoViewModel>> ProductDetailsPost(ProdutoViewModel produtoVM)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            CarrinhoViewModel carrinho = new()
            {
                CarrinhoCabec = new CarrinhoCabecViewModel
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };

            CarrinhoItemViewModel carrinhoItem = new()
            {
                Quantity = produtoVM.Quantidade,
                ProdutoId = produtoVM.Id,
                Produto = await _produtoServico.FindProdutoById(produtoVM.Id, token)
            };

            List<CarrinhoItemViewModel> carrinhoItemsVM = new List<CarrinhoItemViewModel>();
            carrinhoItemsVM.Add(carrinhoItem);
            carrinho.CarrinhoItems = carrinhoItemsVM;

            var result = await _carrinhoServico.AddItemToCarrinhoAsync(carrinho, token);

            if (result is not null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(produtoVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { ErrorMessage = message });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoViewModel pvm)
        {
            var prod = pvm;
            return View();
        }

    }
}