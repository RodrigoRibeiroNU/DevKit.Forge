using Xunit;
using DevKit.Forge.Domain.Entities;

namespace DevKit.Forge.UnitTests.Domain;

public class AnaliseLogTests
{
    [Fact]
    public void ProcessarConteudo_DeveMarcarSucessoEContadoresZerados_QuandoOConteudoForVazio()
    {
        // Arrange
        var analiseLog = new AnaliseLog("arquivo_vazio.log");
        string conteudoVazio = string.Empty;

        // Act
        analiseLog.ProcessarConteudo(conteudoVazio);

        // Assert (Nativo do xUnit: Assert.True e Assert.Equal)
        Assert.True(analiseLog.Sucesso);
        Assert.Equal(0, analiseLog.QtdErros);
        Assert.Equal(0, analiseLog.QtdAvisos);
    }

    [Fact]
    public void ProcessarConteudo_DeveContarErrosEAvisosCorretamente_QuandoOConteudoForValido()
    {
        // Arrange
        var analiseLog = new AnaliseLog("servidor.log");
        var conteudoSimulado = string.Join(Environment.NewLine,
            "[2026-07-09] [INFO] Sistema inicializado.",
            "[2026-07-09] [WARNING] Latencia alta no Redis.", 
            "[2026-07-09] [ERROR] Falha na conexao com a API.", 
            "[2026-07-09] [WARN] Espaco em disco em 90%.", 
            "System.NullReferenceException: Object reference...", 
            "[2026-07-09] [INFO] Fim do processamento."
        );

        // Act
        analiseLog.ProcessarConteudo(conteudoSimulado);

        // Assert (Nativo do xUnit: O valor esperado vem SEMPRE primeiro)
        Assert.True(analiseLog.Sucesso);
        Assert.Equal(2, analiseLog.QtdErros);
        Assert.Equal(2, analiseLog.QtdAvisos);
    }
}