using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Exceptions;
using MiniMercadoSaas.Application.DTO.Request;
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

  

    [HttpPost("{id:guid}/itens")]
        public async Task<IActionResult> AddItem(Guid id, [FromBody] AddItemRequest request)
        {
            try
            {
                var operadorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var venda = await _vendaService.AddItemAsync(id, request, operadorId);

                var response = new VendaDetalheResponse(venda); 

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BusinessException ex) when (ex.Message.Contains("aberta"))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (BusinessException ex) when (ex.Message.Contains("Estoque"))
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
    }
}

