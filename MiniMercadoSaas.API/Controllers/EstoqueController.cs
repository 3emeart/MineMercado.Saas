using System.Security.Claims;
using Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMercadoSaas.Application.DTO.Request;
using MiniMercadoSaas.Application.ServiceInterfaces;

namespace MiniMercadoSaas.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class EstoqueController : ControllerBase
{
    private readonly IEstoqueService _estoqueService;

    public EstoqueController(IEstoqueService estoqueService)
    {
        _estoqueService = estoqueService;
    }

    /// <summary>
    /// Registra uma entrada manual de estoque (somente Gerente/Admin)
    /// </summary>
    [Authorize(Roles = "Gerente,Admin")]
    [HttpPost("entrada")]
    public async Task<IActionResult> RegistrarEntrada([FromBody] EntradaEstoqueRequest request)
    {
        try
        {
            var usuarioId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _estoqueService.RegistrarEntradaAsync(request, usuarioId);
            return Created($"api/v1/estoque/movimentacoes/{response.ProdutoId}", response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (BusinessException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Consulta o histórico de movimentações de estoque de um produto
    /// </summary>
    [HttpGet("movimentacoes/{produtoId:int}")]
    public async Task<IActionResult> ConsultarMovimentacoes(int produtoId)
    {
        var response = await _estoqueService.ConsultarMovimentacoesAsync(produtoId);
        return Ok(response);
    }

    /// <summary>
    /// Lista produtos com estoque abaixo do mínimo
    /// </summary>
    [HttpGet("baixo")]
    public async Task<IActionResult> ConsultarEstoqueBaixo()
    {
        var response = await _estoqueService.ConsultarEstoqueBaixoAsync();
        return Ok(response);
    }
}
