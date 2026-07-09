using MediatR;
using Microsoft.AspNetCore.Mvc;
using DevKit.Forge.Application.Logs.Commands;
using DevKit.Forge.Application.Logs.Queries;

namespace DevKit.Forge.Services.Api.Controllers;

[ApiController]

[Route("api/[controller]")]
public class LogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LogsController(IMediator mediator)

    {
        _mediator = mediator;
    }

    [HttpPost("processar")]
    public async Task<IActionResult> ProcessarLog([FromBody] ProcessarLogCommand command)
    {

        var id = await _mediator.Send(command);

        return Ok(new { mensagem = "Log processado com sucesso!", id });

    }

    [HttpGet]
    public async Task<IActionResult> ObterLogs()
    {
        var query = new ObterAnalisesLogQuery();

        var resultados = await _mediator.Send(query);

        return Ok(resultados);

    }

    [HttpGet("{id:guid}/exportar")]
    public async Task<IActionResult> ExportarRelatorio(Guid id)
    {
        // Dispara a Query através do MediatR
        var relatorio = await _mediator.Send(new ObterRelatorioAnaliseQuery(id));
    
        if (relatorio == null)
        {
            return NotFound(new { mensagem = "Análise de log não encontrada para o ID fornecido." });
        }
    
        // Em vez de retornar um JSON comum na tela, vamos forçar o navegador
        // a fazer o download de um arquivo .json estruturado
        var nomeArquivoDownload = $"relatorio-{relatorio.NomeArquivo}.json";
        
        return File(
            System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(relatorio),
            "application/json",
            nomeArquivoDownload
        );
    }

}

