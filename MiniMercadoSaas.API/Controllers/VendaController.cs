using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MiniMercadoSaas.Application.DTO.Response;
using MiniMercadoSaas.Application.ServiceInterfaces;
using MiniMercadoSaas.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class VendasController : ControllerBase
{
    private readonly IVendaService _vendaService;

    public VendasController(IVendaService vendaService)
    {
        _vendaService = vendaService;
    }

    [HttpPost]
    public async Task<ActionResult<VendaResponse>> PostAsync()
    {
       
        var identity = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(identity) || !Guid.TryParse(identity, out Guid operadorId))
        {
            return Unauthorized("Identificador do operador não encontrado no token.");
        }

        var response = await _vendaService.AbrirAsync(operadorId);

        
        return CreatedAtAction(nameof(GetById), new { id = response.OperadorId }, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Venda>> GetById(Guid id)
    {
        return Ok(); 
    }
}