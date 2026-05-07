using Microsoft.EntityFrameworkCore;
using MiniMercadoSaas.Domain.Entities;

namespace MiniMercadoSaas.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Categoria>()
            .Property(c => c.Nome).HasMaxLength(50).IsRequired();
        
        modelBuilder.Entity<Categoria>()
            .HasMany(c => c.Produtos)
            .WithOne(p => p.Categoria)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Produto>()
            .Property(c => c.Nome).HasMaxLength(50).IsRequired();
        
        modelBuilder.Entity<Produto>()
            .Property(produto => produto.Codigo).HasMaxLength(25).IsRequired();
        
        modelBuilder.Entity<Produto>()
            .Property(produto => produto.PrecoCompra ).HasColumnType("decimal(18,2)").IsRequired();
        
        modelBuilder.Entity<Produto>()
            .Property(produto => produto.PrecoVenda).HasColumnType("decimal(18,2)").IsRequired();
            
        
    }
    
    
    
    
}