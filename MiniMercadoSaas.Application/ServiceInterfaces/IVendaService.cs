using MiniMercadoSaas.Application.DTO.Request;
using MiniMercadoSaas.Application.DTO.Response;
using MiniMercadoSaas.Domain.Entities;

namespace MiniMercadoSaas.Application.ServiceInterfaces;

public interface IVendaService
{
    Task<VendaResponse> AbrirAsync(Guid OperadorId);
    Task <Venda> AddItemAsync(Guid id, AddItemRequest request, Guid operadorId);

}