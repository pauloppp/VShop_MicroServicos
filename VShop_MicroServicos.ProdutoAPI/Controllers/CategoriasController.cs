using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VShop_MicroServicos.ProdutoAPI.Contexto;
using VShop_MicroServicos.ProdutoAPI.DTOs;
using VShop_MicroServicos.ProdutoAPI.Models;
using VShop_MicroServicos.ProdutoAPI.Servicos.Interfaces;

namespace VShop_MicroServicos.ProdutoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaServico _categoriaServico;

        public CategoriasController(ICategoriaServico categoriaServico)
        {
            _categoriaServico = categoriaServico;
        }


        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
        {
            var categoriasDTO = await _categoriaServico.GetCategorias();

            if (categoriasDTO is null)
            {
                return NotFound("Categorias não encontradas");
            }
            return Ok(categoriasDTO);
        }

        // GET: api/Categorias/Produtos
        [HttpGet("Produtos")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoriasProdutos()
        {
            var categoriasDTO = await _categoriaServico.GetCategoriasProdutos();

            if (categoriasDTO is null)
            {
                return NotFound("CategoriasProdutos não encontradas");
            }
            return Ok(categoriasDTO);
        }

        // GET: api/Categorias/Produtos/Id
        [HttpGet("{id:int}", Name = "GetCategoria")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoriaById(int id)
        {
            var categoriaDTO = await _categoriaServico.GetCategoriaById(id);

            if (categoriaDTO is null)
            {
                return NotFound("Categoria não encontrada");
            }
            return Ok(categoriaDTO);
        }

        // POST: api/Categoria
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria([FromBody] CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO == null)
            {
                return BadRequest("Dados Inválidos");
            }
            await _categoriaServico.AddCategoria(categoriaDTO);

            return new CreatedAtRouteResult("GetCategoria", new { id = categoriaDTO.Id }, categoriaDTO);
        }

        // PUT: api/Categoria/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCategoria(int id, [FromBody] CategoriaDTO categoriaDTO)
        {
            if (id != categoriaDTO.Id)
            {
                return BadRequest();
            }

            if (categoriaDTO is null)
            {
                return BadRequest();
            }

            await _categoriaServico.UpdateCategoria(categoriaDTO);
            return Ok(categoriaDTO);
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoriaDTO = await _categoriaServico.GetCategoriaById(id);

            if (categoriaDTO is null)
            {
                return NotFound("Categoria não encontrada");
            }

            await _categoriaServico.RemoveCategoria(id);
            return Ok(categoriaDTO);
        }
                
    }
}
