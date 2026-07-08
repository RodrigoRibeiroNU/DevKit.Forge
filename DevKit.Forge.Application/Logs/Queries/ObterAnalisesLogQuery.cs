using MediatR;



namespace DevKit.Forge.Application.Logs.Queries;



public record AnaliseLogDto(Guid Id, string NomeArquivo, DateTime DataProcessamento, bool Sucesso, int QtdErros, int QtdAvisos);


public class ObterAnalisesLogQuery : IRequest<IEnumerable<AnaliseLogDto>>

{

}

