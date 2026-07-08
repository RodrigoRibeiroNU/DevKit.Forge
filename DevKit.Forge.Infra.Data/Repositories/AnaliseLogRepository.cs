using DevKit.Forge.Domain.Entities;
using DevKit.Forge.Domain.Interfaces;
using DevKit.Forge.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevKit.Forge.Infra.Data.Repositories;

public class AnaliseLogRepository : IAnaliseLogRepository
{
    private readonly AppDbContext _context;

    public AnaliseLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(AnaliseLog analise)
    {
        await _context.Analises.AddAsync(analise);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AnaliseLog>> ObterTodosAsync()
    {
        // Consultas somente leitura não exigem change tracking.
        return await _context.Analises.AsNoTracking().ToListAsync();
    }
}
