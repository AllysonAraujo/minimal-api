using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Db.Configurations;

/// <summary>
/// Configuração da entidade Veiculo usando Fluent API
/// </summary>
public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        // Nome da tabela
        builder.ToTable("Veiculos");
        
        // Configurações da chave primária
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .ValueGeneratedOnAdd();
        
        // Configurações das propriedades
        builder.Property(v => v.Nome)
            .IsRequired()
            .HasMaxLength(150)
            .HasComment("Nome/modelo do veículo");
        
        builder.Property(v => v.Marca)
            .IsRequired()
            .HasMaxLength(100)
            .HasComment("Marca/fabricante do veículo");
        
        builder.Property(v => v.Ano)
            .IsRequired()
            .HasComment("Ano de fabricação do veículo");
        
        // Validação para ano (MySQL) - nova sintaxe
        builder.ToTable(t => t.HasCheckConstraint("CK_Veiculo_Ano", "Ano >= 1900 AND Ano <= YEAR(CURDATE()) + 1"));
        
        // Índices para otimizar consultas
        builder.HasIndex(v => v.Marca)
            .HasDatabaseName("IX_Veiculos_Marca");
            
        builder.HasIndex(v => v.Ano)
            .HasDatabaseName("IX_Veiculos_Ano");
            
        builder.HasIndex(v => new { v.Marca, v.Nome })
            .HasDatabaseName("IX_Veiculos_Marca_Nome");
    }
}
