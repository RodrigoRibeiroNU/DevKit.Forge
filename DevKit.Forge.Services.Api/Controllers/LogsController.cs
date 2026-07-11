using MediatR;
using Microsoft.AspNetCore.Mvc;
using DevKit.Forge.Application.Logs.Commands;
using DevKit.Forge.Application.Logs.Queries;
using System.Text.Json;
using System.Text.Encodings.Web;
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
        var resultadoDto = await _mediator.Send(new ObterRelatorioAnaliseQuery(id));
    
        if (resultadoDto == null)
        {
            return NotFound(new { mensagem = "Análise de log não encontrada para o ID fornecido." });
        }
    
        var opcoesSerializacao = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // 👈 A mágica para aceitar acentos aqui!
            WriteIndented = true // Deixa o JSON formatado com quebras de linha e recuos (bonito para leitura)
        };
        
        // 3. Serializamos o objeto para string usando as opções configuradas
        string jsonString = JsonSerializer.Serialize(resultadoDto, opcoesSerializacao);
        byte[] bytesArquivo = System.Text.Encoding.UTF8.GetBytes(jsonString);
        
        // 4. Retorna o arquivo limpo para o Angular
        return File(bytesArquivo, "application/json", $"relatorio-{resultadoDto.NomeArquivo}.json");
    }

}

