using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Db;

/// <summary>
/// Extensões para o DbContexto que facilitam queries comuns
/// </summary>
public static class DbContextoExtensions
{
    /// <summary>
    /// Retorna uma query de administradores sem tracking para consultas de leitura
    /// </summary>
    public static IQueryable<Administrador> AdministradoresReadOnly(this DbContexto context)
    {
        return context.Administradores.AsNoTracking();
    }
    
    /// <summary>
    /// Retorna uma query de veículos sem tracking para consultas de leitura
    /// </summary>
    public static IQueryable<Veiculo> VeiculosReadOnly(this DbContexto context)
    {
        return context.Veiculos.AsNoTracking();
    }
    
    /// <summary>
    /// Busca um administrador por email de forma otimizada
    /// </summary>
    public static async Task<Administrador?> BuscarAdministradorPorEmailAsync(
        this DbContexto context, 
        string email,
        CancellationToken cancellationToken = default)
    {
        return await context.Administradores
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
    }
    
    /// <summary>
    /// Busca veículos por marca de forma otimizada
    /// </summary>
    public static async Task<List<Veiculo>> BuscarVeiculosPorMarcaAsync(
        this DbContexto context, 
        string marca,
        CancellationToken cancellationToken = default)
    {
        return await context.Veiculos
            .AsNoTracking()
            .Where(v => v.Marca.ToLower().Contains(marca.ToLower()))
            .OrderBy(v => v.Nome)
            .ToListAsync(cancellationToken);
    }
    
    /// <summary>
    /// Busca veículos por ano de forma otimizada
    /// </summary>
    public static async Task<List<Veiculo>> BuscarVeiculosPorAnoAsync(
        this DbContexto context, 
        int ano,
        CancellationToken cancellationToken = default)
    {
        return await context.Veiculos
            .AsNoTracking()
            .Where(v => v.Ano == ano)
            .OrderBy(v => v.Marca)
            .ThenBy(v => v.Nome)
            .ToListAsync(cancellationToken);
    }
}
