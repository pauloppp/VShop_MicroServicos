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
        public HomeController(ILogger<HomeController> logger,
            IProdutoServico productService)
        {
            _logger = logger;
            _produtoServico = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _produtoServico.GetAllProdutos(string.Empty);

            if (products is null)
                return View("Error");

            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult<ProdutoViewModel>> ProductDetails(int id)
        {
            var product = await _produtoServico.FindProdutoById(id, string.Empty);

            if (product is null)
                return View("Error");

            return View(product);
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