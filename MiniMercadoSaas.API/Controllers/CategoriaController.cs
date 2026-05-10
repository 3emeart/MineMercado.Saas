using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMercadoSaas.Application.DTO.Request;
using MiniMercadoSaas.Application.DTO.Response;
using MiniMercadoSaas.Application.ServiceInterfaces;
using MiniMercadoSaas.Domain.Entities;
using MiniMercadoSaas.Infrastructure.Context;

namespace MiniMercadoSaas.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriaController : ControllerBase
{

    private readonly ICategoryService _categoryService;

    public CategoriaController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CategoriaCreateRequest request)
    {
        var novaCategoria = await _categoryService.CriarCategoria(request);
        return Ok(novaCategoria);
    }

    [HttpGet]

    public async Task<IActionResult> Get()
    {
        var categorias = await _categoryService.ListarCategorias();
        return Ok(categorias);
    }

    [HttpGet("{nome}")]

    public async Task<IActionResult> GetByName(string nome)
    {
        var categoriaName = await _categoryService.BuscarCategoriaPorNome(nome);
        return Ok(categoriaName);
    }

    [HttpDelete]

    public async Task<IActionResult> Deletar(int id)
    {
        await _categoryService.DeletarCategoria(id);
        return Ok();
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Editar(int id,[FromBody] CategoriaCreateRequest request)
    {
        var categorias = await _categoryService.AtualizarCategoria(id, request);
        return Ok(categorias);
        
        
    }






}