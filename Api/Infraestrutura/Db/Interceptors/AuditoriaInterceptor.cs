using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Db.Interceptors;

/// <summary>
/// Interceptor para preencher automaticamente campos de auditoria
/// </summary>
public class AuditoriaInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PreencherCamposAuditoria(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        PreencherCamposAuditoria(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void PreencherCamposAuditoria(DbContext? context)
    {
        if (context == null) return;

        var entidades = context.ChangeTracker.Entries<EntidadeBase>();
        var dataAtual = DateTime.UtcNow;

        foreach (var entrada in entidades)
        {
            switch (entrada.State)
            {
                case EntityState.Added:
                    entrada.Entity.DataCriacao = dataAtual;
                    // Em uma aplicação real, você pegaria o usuário do contexto de segurança
                    entrada.Entity.CriadoPor = "Sistema"; 
                    break;
                    
                case EntityState.Modified:
                    entrada.Entity.DataAtualizacao = dataAtual;
                    // Em uma aplicação real, você pegaria o usuário do contexto de segurança
                    entrada.Entity.AtualizadoPor = "Sistema";
                    break;
            }
        }
    }
}
