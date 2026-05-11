using MiniMercadoSaas.Domain.Enums;

namespace MiniMercadoSaas.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string SenhaHash { get; set; }
    public bool Active { get; set; }
    public Role Role { get; set; }
    public DateTime CreatedIn { get; set; } = DateTime.UtcNow;
    public DateTime UltimoLoginIn { get; set; } 
}