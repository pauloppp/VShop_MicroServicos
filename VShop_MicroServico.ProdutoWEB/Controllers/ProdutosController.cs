using Microsoft.AspNetCore.Authentication;
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
    [Authorize(Roles = Role.Admin)]
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
            var tokenAcesso = await GetAccessToken();
            var result = await _produtoServico.GetAllProdutos(tokenAcesso);
            if (result is null)
            {
                return View("Error");
            }
            return View(result);
        }

        // GET: ProdutosController/Create
        public async Task<IActionResult> Create()
        {
            var tokenAcesso = await GetAccessToken();
            ViewBag.CategoriaId = new SelectList(await _categoriaServico.GetAllCategorias(tokenAcesso), "Id", "Nome");
            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var tokenAcesso = await GetAccessToken();
                var result = await _produtoServico.CreateProduto(produtoViewModel, tokenAcesso);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var tokenAcesso = await GetAccessToken();
                ViewBag.CategoriaId = new SelectList(await _categoriaServico.GetAllCategorias(tokenAcesso), "Id", "Nome");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tokenAcesso = await GetAccessToken();
            ViewBag.CategoriaId = new SelectList(await _categoriaServico.GetAllCategorias(tokenAcesso), "Id", "Nome");

            var result = await _produtoServico.FindProdutoById(id, tokenAcesso);

            if (result is null) return View("Error");

            return View(result);
        }

        // POST: ProdutosController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Edit(ProdutoViewModel produtoViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //CultureInfo culture = CultureInfo.CurrentCulture;
                    //Console.WriteLine("The current culture is {0} [{1}]",
                    //culture.NativeName, culture.Name);



                    var culture = CultureInfo.CurrentCulture; //new CultureInfo("pt-BR");
                    string convertPreco = ((double)produtoViewModel.Preco).ToString();
                    decimal preco = Convert.ToDecimal(convertPreco, culture);
                    produtoViewModel.Preco = preco;


                    ModelState.ClearValidationState(nameof(ProdutoViewModel));
                    if (!TryValidateModel(produtoViewModel, nameof(ProdutoViewModel)))
                    {
                        //return Page();
                    }

                    var tokenAcesso = await GetAccessToken();
                    var result = await _produtoServico.UpdateProduto(produtoViewModel, tokenAcesso);
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
        //[Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var tokenAcesso = await GetAccessToken();
            var result = await _produtoServico.FindProdutoById(id, tokenAcesso);

            if (result is null) return View("Error");

            return View(result);
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var tokenAcesso = await GetAccessToken();
                var result = await _produtoServico.DeleteProdutoById(id, tokenAcesso);

                if (!result) return View("Error");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new Exception();
            }
        }

        // .........>
        private async Task<string> GetAccessToken()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }

    }
}
