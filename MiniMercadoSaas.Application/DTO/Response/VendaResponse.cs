using MiniMercadoSaas.Domain.Enums;

namespace MiniMercadoSaas.Application.DTO.Response;

public class VendaResponse(Guid vendaId, decimal vendaTotalFinal, DateTime vendaAbertaEm, StatusVenda vendaStatus, Guid vendaOperadorId)
{
    public Guid OperadorId { get; set; }
    public decimal TotalFinal { get; set; }
    public DateTime AbertaEm { get; set; }
    public StatusVenda Status { get; set; }
}
    
    
    