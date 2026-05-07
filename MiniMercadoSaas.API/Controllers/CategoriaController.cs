using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMercadoSaas.Application.DTO.Request;
using MiniMercadoSaas.Application.DTO.Response;
using MiniMercadoSaas.Domain.Entities;
using MiniMercadoSaas.Infrastructure.Context;

namespace MiniMercadoSaas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    
    private readonly AppDbContext _context;
    
    public CategoriaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CategoriaResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Categoria>>> GetAll()
    {
        var categorias = await _context.Categorias.ToListAsync();
        return Ok(categorias);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoriaCreateRequest categoriaRequest)
    {
        var novaCategoria = new Categoria()
        {
            Nome = categoriaRequest.Nome,
            Descricao = categoriaRequest.Descricao,
        };
        await _context.Categorias.AddAsync(novaCategoria);
        await _context.SaveChangesAsync();
        return Ok(novaCategoria);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] CategoriaCreateRequest categoriaRequest)
    {
        var categoriaNoBanco = await _context.Categorias.FindAsync(id);
        if (categoriaNoBanco is null)
        {
            return NotFound("Categoria não encontrada");
        }
        
        categoriaNoBanco.Nome = categoriaRequest.Nome;
        categoriaNoBanco.Descricao = categoriaRequest.Descricao;
        
        await _context.SaveChangesAsync();
        return Ok(categoriaNoBanco);



    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletarCategoria = await _context.Categorias.FindAsync(id);
        if (deletarCategoria is null)
            return NotFound("Categoria não encontrada");
        
        _context.Categorias.Remove(deletarCategoria);
        
        await _context.SaveChangesAsync();
        return NoContent();

    }
   
    
    


}