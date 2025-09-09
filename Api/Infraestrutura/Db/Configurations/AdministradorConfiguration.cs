using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;

namespace MinimalApi.Infraestrutura.Db.Configurations;

/// <summary>
/// Configuração da entidade Administrador usando Fluent API
/// </summary>
public class AdministradorConfiguration : IEntityTypeConfiguration<Administrador>
{
    public void Configure(EntityTypeBuilder<Administrador> builder)
    {
        // Nome da tabela
        builder.ToTable("Administradores");
        
        // Configurações da chave primária
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();
        
        // Configurações das propriedades
        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(255)
            .HasComment("Email único do administrador");
        
        builder.Property(a => a.Senha)
            .IsRequired()
            .HasMaxLength(255) // Aumentado para comportar hash
            .HasComment("Senha do administrador (deve ser armazenada como hash)");
        
        builder.Property(a => a.Perfil)
            .IsRequired()
            .HasConversion<string>() // Converte enum para string
            .HasMaxLength(10)
            .HasComment("Perfil de acesso do administrador");
        
        // Índice único para email
        builder.HasIndex(a => a.Email)
            .IsUnique()
            .HasDatabaseName("IX_Administradores_Email");
        
        // Dados iniciais (Seed Data)
        builder.HasData(
            new Administrador 
            {
                Id = 1,
                Email = "administrador@teste.com",
                Senha = "123456", // Em produção, use hash da senha
                Perfil = Perfil.Adm
            }
        );
    }
}
