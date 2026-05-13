using MiniMercadoSaas.Domain.Entities;
using MiniMercadoSaas.Domain.Enums;

namespace MiniMercadoSaas.Application.DTO.Response;

public class VendaDetalheResponse(Venda venda)
{
    public Guid Id { get; set; }
    public StatusVenda StatusVenda { get; set; }
    public decimal TotalFinal { get; set; }
    public DateTime AbertaEm { get; set; }
    public ICollection<ItemVenda>  Itens { get; set; }
}

