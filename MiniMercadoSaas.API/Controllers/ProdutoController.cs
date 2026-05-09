using Microsoft.AspNetCore.Mvc;
using MiniMercadoSaas.Application.DTO.Request;
using MiniMercadoSaas.Application.DTO.Response;
using MiniMercadoSaas.Application.ServiceInterfaces;

namespace MiniMercadoSaas.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]

public class ProdutoController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProdutoController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProdutoResponse>>> BuscarTodos()
    {
        var produtos = await _productService.BuscarTodos();
        return Ok(produtos);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoResponse>> Create(ProdutoRequest produtoRequest)
    {
       var novoProduto =  await _productService.Create(produtoRequest);
        return Ok(novoProduto);
    }
    
    
    
    
}