using Microsoft.EntityFrameworkCore;
using MiniMercadoSaas.Domain.Entities;
using MiniMercadoSaas.Domain.Interfaces;
using MiniMercadoSaas.Infrastructure.Context;

namespace MiniMercadoSaas.Infrastructure.Repositorys;

public class VendaRepository : IVendaRepository
{
    private readonly AppDbContext _dbContext;

    public VendaRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Venda?> GetByIdAsync(Guid id, bool includeItens = true)
    {
        IQueryable<Venda> query = _dbContext.Vendas;

        if (includeItens)
        {
            query = query.Include(v => v.Itens)
                .ThenInclude(i => i.Produto);

        }

        return await query.FirstOrDefaultAsync(v => v.Id == id);

    }

    public async Task AddAsync(Venda venda)
    {
        await _dbContext.Vendas.AddAsync(venda);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Venda venda)
    {
         _dbContext.Update(venda);
        await _dbContext.SaveChangesAsync();
    }

   
}