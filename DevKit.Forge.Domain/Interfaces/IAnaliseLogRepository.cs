using DevKit.Forge.Domain.Entities;

namespace DevKit.Forge.Domain.Interfaces;

public interface IAnaliseLogRepository
{
    Task AdicionarAsync(AnaliseLog analise);
    Task<IEnumerable<AnaliseLog>> ObterTodosAsync(); 
}