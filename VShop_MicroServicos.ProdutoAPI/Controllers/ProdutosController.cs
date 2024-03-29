﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VShop_MicroServico.ProdutoAPI.Roles;
using VShop_MicroServicos.ProdutoAPI.Contexto;
using VShop_MicroServicos.ProdutoAPI.DTOs;
using VShop_MicroServicos.ProdutoAPI.Models;
using VShop_MicroServicos.ProdutoAPI.Servicos.Interfaces;

namespace VShop_MicroServicos.ProdutoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoServico _produtoServico;

        public ProdutosController(IProdutoServico produtoServico)
        {
            _produtoServico = produtoServico;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            var produtosDTO = await _produtoServico.GetProdutos();

            if (produtosDTO is null)
            {
                return NotFound("Produtos não encontrados");
            }
            return Ok(produtosDTO);
        }

        // GET: api/Produtos/Id
        [HttpGet("{id:int}", Name = "GetProduto")]
        public async Task<ActionResult<ProdutoDTO>> GetProdutoById(int id)
        {
            var produtoDTO = await _produtoServico.GetProdutosById(id);

            if (produtoDTO is null)
            {
                return NotFound("Produto não encontrado");
            }
            return Ok(produtoDTO);
        }

        // POST: api/Produto
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<Categoria>> PostProduto([FromBody] ProdutoDTO produtoDTO)
        {
            if (produtoDTO == null)
            {
                return BadRequest("Dados Inválidos");
            }
            await _produtoServico.AddProduto(produtoDTO);

            return new CreatedAtRouteResult("GetProduto", new { id = produtoDTO.Id }, produtoDTO);
        }

        // PUT: api/Produto/5
        [HttpPut]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> PutProduto([FromBody] ProdutoDTO produtoDTO)
        {
            if (produtoDTO is null)
            {
                return BadRequest();
            }

            await _produtoServico.UpdateProduto(produtoDTO);
            return Ok(produtoDTO);
        }

        // DELETE: api/Produto/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produtoDTO = await _produtoServico.GetProdutosById(id);

            if (produtoDTO is null)
            {
                return NotFound("Produto não encontrado");
            }

            await _produtoServico.RemoveProduto(id);
            return Ok(produtoDTO);
        }

    }
}
