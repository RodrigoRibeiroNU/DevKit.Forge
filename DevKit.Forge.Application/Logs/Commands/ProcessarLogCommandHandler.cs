using MediatR;
using DevKit.Forge.Domain.Entities;
using DevKit.Forge.Domain.Interfaces;

namespace DevKit.Forge.Application.Logs.Commands;

public class ProcessarLogCommandHandler : IRequestHandler<ProcessarLogCommand, Guid>
{
    private readonly IAnaliseLogRepository _repository;

    public ProcessarLogCommandHandler(IAnaliseLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(ProcessarLogCommand request, CancellationToken cancellationToken)
    {
        var analise = new AnaliseLog(request.NomeArquivo);

        // A análise textual ocorre no domínio antes da persistência para manter a regra de negócio centralizada.
        analise.ProcessarConteudo(request.ConteudoArquivo);

        await _repository.AdicionarAsync(analise);
    
        return analise.Id;
    }
}