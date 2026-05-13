using Exceptions;
using MiniMercadoSaas.Application.DTO.Request;
using MiniMercadoSaas.Application.DTO.Response;
using MiniMercadoSaas.Application.ServiceInterfaces;
using MiniMercadoSaas.Domain;
using MiniMercadoSaas.Domain.Entities;
using MiniMercadoSaas.Domain.Enums;
using MiniMercadoSaas.Domain.Interfaces;

namespace MiniMercadoSaas.Application.Services;

public class VendaService : IVendaService
{
       private readonly IVendaRepository _vendaRepository;
       private readonly IProductRepository _productRepository;

       public VendaService(IVendaRepository vendaRepository, IProductRepository productRepository)
       {
              _vendaRepository = vendaRepository;
              _productRepository = _productRepository;
       }

       public async Task<VendaResponse> AbrirAsync(Guid OperadorId)
       {
              var venda = new Venda
              {
                     OperadorId = OperadorId,
                     Status = StatusVenda.Aberta,
                     TotalFinal = 0,
                     AbertaEm = DateTime.UtcNow,


              };

              await _vendaRepository.AddAsync(venda);

              return new VendaResponse(

                     venda.Id,
                     venda.TotalFinal,
                     venda.AbertaEm,
                     venda.Status,
                     venda.OperadorId
              );



       }

       public async Task<Venda> AddItemAsync(Guid id, AddItemRequest request, Guid operadorId)
       {
              var vendasExistentes = await _vendaRepository.GetByIdAsync(id, true)
                                     ?? throw new KeyNotFoundException("Venda não encontrada");

              if (vendasExistentes.Status != StatusVenda.Aberta)
              {
                     throw new BusinessException("A venda não está aberta para adição de itens");
              }

              var produto = await _productRepository.FindByIdAsync(request.ProdutoId)
                            ?? throw new NotFoundException("Produto não encontrado");

              if (!produto.Ativo)
              {
                     throw new BusinessException("Produto não está ativo");
              }

              if (produto.Quantidade < request.Quantidade)
              {
                     throw new BusinessException("Estoque insuficiente");
              }

              var itemFinal = new ItemVenda(produto.Id, request.Quantidade, produto.PrecoVenda);

              vendasExistentes.AdicionarItem(itemFinal);
              
              await _vendaRepository.UpdateAsync(vendasExistentes);
              return vendasExistentes;

       }
       
       

}