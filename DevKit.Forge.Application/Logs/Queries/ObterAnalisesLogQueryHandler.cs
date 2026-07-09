using MediatR;
using DevKit.Forge.Domain.Interfaces;

namespace DevKit.Forge.Application.Logs.Queries;

public class ObterAnalisesLogQueryHandler : IRequestHandler<ObterAnalisesLogQuery, IEnumerable<AnaliseLogDto>>
{
    private readonly IAnaliseLogRepository _repository;

    public ObterAnalisesLogQueryHandler(IAnaliseLogRepository repository)

    {

        _repository = repository;

    }



    public async Task<IEnumerable<AnaliseLogDto>> Handle(ObterAnalisesLogQuery request, CancellationToken cancellationToken)

    {

        var analises = await _repository.ObterTodosAsync();



        return analises.Select(a => new AnaliseLogDto(

            a.Id,

            a.NomeArquivo,

            a.DataProcessamento,

            a.Sucesso,

            a.QtdErros,

            a.QtdAvisos

        ));

    }

}

