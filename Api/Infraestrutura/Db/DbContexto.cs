using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using MinimalApi.Infraestrutura.Db.Interceptors;

namespace MinimalApi.Infraestrutura.Db;

/// <summary>
/// Contexto de dados da aplicação que gerencia as entidades do sistema
/// </summary>
public class DbContexto : DbContext
{
    private readonly IConfiguration _configuracaoAppSettings;
    
    /// <summary>
    /// Construtor que recebe a configuração da aplicação
    /// </summary>
    /// <param name="configuracaoAppSettings">Configurações da aplicação</param>
    public DbContexto(IConfiguration configuracaoAppSettings)
    {
        _configuracaoAppSettings = configuracaoAppSettings;
    }

    public DbSet<Administrador> Administradores { get; set; } = default!;
    public DbSet<Veiculo> Veiculos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas as configurações do assembly atual automaticamente
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContexto).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var stringConexao = _configuracaoAppSettings.GetConnectionString("MySql");
            
            if (string.IsNullOrEmpty(stringConexao))
            {
                throw new InvalidOperationException(
                    "String de conexão 'MySql' não foi encontrada nas configurações da aplicação.");
            }
            
            optionsBuilder.UseMySql(
                stringConexao,
                ServerVersion.AutoDetect(stringConexao),
                options =>
                {
                    // Configurações adicionais do MySQL
                    options.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                }
            );
            
            // Adiciona interceptors
            optionsBuilder.AddInterceptors(new AuditoriaInterceptor());
            
            // Configurações de logging e performance para desenvolvimento
            if (_configuracaoAppSettings.GetValue<string>("Environment") == "Development")
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    .LogTo(Console.WriteLine);
            }
        }
    }
}