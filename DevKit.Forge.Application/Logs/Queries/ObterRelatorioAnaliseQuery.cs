using MediatR;

namespace DevKit.Forge.Application.Logs.Queries;

public record ObterRelatorioAnaliseQuery(Guid AnaliseId) : IRequest<RelatorioAnaliseDto?>;

public record RelatorioAnaliseDto(
    Guid Id,
    string NomeArquivo,
    DateTime DataUpload,
    int TotalErros,
    int TotalAvisos,
    bool ProcessadoComSucesso,
    string StatusSaude
);