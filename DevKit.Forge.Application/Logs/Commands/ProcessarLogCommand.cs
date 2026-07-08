using MediatR;

namespace DevKit.Forge.Application.Logs.Commands;

public class ProcessarLogCommand : IRequest<Guid>
{
    public string NomeArquivo { get; set; } = string.Empty;
    public string ConteudoArquivo { get; set; } = string.Empty;
}
