using MiniMercadoSaas.Domain.Entities;

namespace MiniMercadoSaas.Domain.Interfaces;

public interface IVendaRepository
{
    Task<Venda> GetByIdAsync(Guid vendaId, bool includeItens);
    Task AddAsync(Venda venda);
    Task UpdateAsync(Venda venda);
}