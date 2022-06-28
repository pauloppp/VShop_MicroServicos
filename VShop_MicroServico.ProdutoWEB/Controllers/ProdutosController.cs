using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using VShop_MicroServico.ProdutoWEB.Models;
using VShop_MicroServico.ProdutoWEB.Roles;
using VShop_MicroServico.ProdutoWEB.Servicos.Interfaces;

namespace VShop_MicroServico.ProdutoWEB.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoServico _produtoServico;
        private readonly ICategoriaServico _categoriaServico;

        public ProdutosController(IProdutoServico produtoServico, ICategoriaServico categoriaServico)
        {
            _produtoServico = produtoServico;
            _categoriaServico = categoriaServico;
        }

        // GET: ProdutosController
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
        {
            var result = await _produtoServico.GetAllProdutos();
            if (result is null)
            {
                return View("Error");
            }
            return View(result);
        }

        // GET: ProdutosController/Create
        public async Task<IActionResult> Create()
        {

            ViewBag.CategoriaId = new SelectList(await _categoriaServico.GetAllCategorias(), "Id", "Nome");
            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _produtoServico.CreateProduto(produtoViewModel);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(await _categoriaServico.GetAllCategorias(), "Id", "Nome");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.CategoriaId = new SelectList(await _categoriaServico.GetAllCategorias(), "Id", "Nome");

            var result = await _produtoServico.FindProdutoById(id);

            if (result is null) return View("Error");

            return View(result);
        }

        // POST: ProdutosController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(ProdutoViewModel produtoViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    //CultureInfo culture = CultureInfo.CurrentCulture;
                    //Console.WriteLine("The current culture is {0} [{1}]",
                    //                  culture.NativeName, culture.Name);


                    var culture = CultureInfo.CurrentCulture; //new CultureInfo("pt-BR");
                    string convertPreco = ((double)produtoViewModel.Preco).ToString();
                    decimal preco = Convert.ToDecimal(convertPreco, culture);
                    produtoViewModel.Preco = preco;

                    var result = await _produtoServico.UpdateProduto(produtoViewModel);
                    if (result is not null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(produtoViewModel);
            }
            catch
            {
                throw new Exception();
            }
        }

        // GET: ProdutosController/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _produtoServico.FindProdutoById(id);

            if (result is null) return View("Error");

            return View(result);
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var result = await _produtoServico.DeleteProdutoById(id);

                if (!result) return View("Error");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new Exception();
            }
        }

    }
}
