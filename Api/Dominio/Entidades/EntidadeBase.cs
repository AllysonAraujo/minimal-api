namespace MinimalApi.Dominio.Entidades;

/// <summary>
/// Classe base para entidades que precisam de auditoria
/// </summary>
public abstract class EntidadeBase
{
    public int Id { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataAtualizacao { get; set; }
    public string? CriadoPor { get; set; }
    public string? AtualizadoPor { get; set; }
}
