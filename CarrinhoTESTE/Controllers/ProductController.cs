using Microsoft.AspNetCore.Mvc;

namespace CarrinhoTESTE.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
