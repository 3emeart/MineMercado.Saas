using MiniMercadoSaas.Domain.Entities;

namespace MiniMercadoSaas.Domain.Interfaces;

public interface IVendaRepository
{
    Task<Venda> GetByIdAsync(Guid id, bool includeItens);
    Task AddAsync(Venda venda);
    Task UpdateAsync(Venda venda);
}