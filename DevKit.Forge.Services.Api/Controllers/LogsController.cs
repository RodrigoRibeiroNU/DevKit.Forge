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

}

