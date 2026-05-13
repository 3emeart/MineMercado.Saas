using MiniMercadoSaas.Application.DTO.Response;
using MiniMercadoSaas.Application.ServiceInterfaces;
using MiniMercadoSaas.Domain.Entities;
using MiniMercadoSaas.Domain.Enums;
using MiniMercadoSaas.Domain.Interfaces;

namespace MiniMercadoSaas.Application.Services;

public class VendaService : IVendaService
{
       private readonly IVendaRepository _vendaRepository;

       public VendaService(IVendaRepository vendaRepository)
       {
              _vendaRepository = vendaRepository;
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

  
}