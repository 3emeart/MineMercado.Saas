namespace MiniMercadoSaas.Domain.Entities;

public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    
    public DateTime DataCriacao { get; set; }
    
    public ICollection<Produto> Produtos { get; set; }
}