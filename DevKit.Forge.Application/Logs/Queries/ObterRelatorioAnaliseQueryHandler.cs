using MediatR;
using DevKit.Forge.Domain.Entities;
using DevKit.Forge.Domain.Interfaces;

namespace DevKit.Forge.Application.Logs.Queries;

public class ObterRelatorioAnaliseQueryHandler : IRequestHandler<ObterRelatorioAnaliseQuery, RelatorioAnaliseDto?>
{
    private readonly IAnaliseLogRepository _repository;

    public ObterRelatorioAnaliseQueryHandler(IAnaliseLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<RelatorioAnaliseDto?> Handle(ObterRelatorioAnaliseQuery request, CancellationToken cancellationToken)
    {
        var todasAnalises = await _repository.ObterTodosAsync();

        var analise = todasAnalises.FirstOrDefault(x => x.Id == request.AnaliseId);
        
        if (analise == null) return null;

        // Regra de saúde do log baseada nos contadores da Entidade de Domínio
        string saude = analise.QtdErros switch
        {
            0 when analise.QtdAvisos == 0 => "Saudável (Clean)",
            0 => "Atenção (Avisos Detectados)",
            < 5 => "Instável (Erros Críticos Baixos)",
            _ => "Crítico (Múltiplas Falhas)"
        };

        return new RelatorioAnaliseDto(
            analise.Id,
            analise.NomeArquivo,
            analise.DataProcessamento,
            analise.QtdErros,
            analise.QtdAvisos,
            analise.Sucesso,
            saude
        );
    }
}