using Xunit;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevKit.Forge.Application.Logs.Queries;
using DevKit.Forge.Domain.Interfaces;
using DevKit.Forge.Domain.Entities;
using System.Reflection;

namespace DevKit.Forge.UnitTests.Application;

public class ObterRelatorioAnaliseQueryHandlerTests
{
    private readonly Mock<IAnaliseLogRepository> _repositoryMock;
    private readonly ObterRelatorioAnaliseQueryHandler _handler;

    public ObterRelatorioAnaliseQueryHandlerTests()
    {
        _repositoryMock = new Mock<IAnaliseLogRepository>();
        _handler = new ObterRelatorioAnaliseQueryHandler(_repositoryMock.Object);
    }

    // Método usando Reflexão para burlar os setters privados e criar o objeto real de domínio
    private AnaliseLog CriarAnaliseLogViaReflection(Guid id, string nomeArquivo, int qtdErros, int qtdAvisos, bool sucesso)
    {
        // Cria a instância mesmo com o construtor privado/protected
        var analise = (AnaliseLog)Activator.CreateInstance(typeof(AnaliseLog), true)!;

        // Injeta os valores diretamente nos campos/propriedades usando reflexão
        DefinirPropriedade(analise, "Id", id);
        DefinirPropriedade(analise, "NomeArquivo", nomeArquivo);
        DefinirPropriedade(analise, "QtdErros", qtdErros);
        DefinirPropriedade(analise, "QtdAvisos", qtdAvisos);
        DefinirPropriedade(analise, "Sucesso", sucesso);

        return analise;
    }

    private void DefinirPropriedade(object obj, string nomePropriedade, object valor)
    {
        var prop = obj.GetType().GetProperty(nomePropriedade, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (prop != null && prop.CanWrite)
        {
            prop.SetValue(obj, valor, null);
        }
        else
        {
            // Se for uma propriedade automática com private set/init, tentamos pelo backing field gerado pelo compilador
            var field = obj.GetType().GetField($"<{nomePropriedade}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(obj, valor);
        }
    }

    [Fact]
    public async Task Deve_Retornar_Status_Saudavel_Quando_Nao_Houver_Erros_Ou_Avisos()
    {
        // Arrange
        var logId = Guid.NewGuid();
        var query = new ObterRelatorioAnaliseQuery(logId);
        var logFake = CriarAnaliseLogViaReflection(logId, "teste-saudavel.log", 0, 0, true);

        // Mockamos o ObterTodosAsync para retornar uma lista com o nosso log fake
        _repositoryMock
            .Setup(r => r.ObterTodosAsync())
            .ReturnsAsync(new List<AnaliseLog> { logFake });

        // Act
        var resultado = await _handler.Handle(query, CancellationToken.None);

        // Assert
        resultado.Should().NotBeNull();
        resultado.StatusSaude.Should().Contain("Saudável");
    }

    [Theory]
    [InlineData(3, 2, "Instável")]
    [InlineData(10, 5, "Crítico")]
    public async Task Deve_Calcular_Status_Saude_Corretamente_Baseado_Nas_Contagens(int erros, int avisos, string statusEsperado)
    {
        // Arrange
        var logId = Guid.NewGuid();
        var query = new ObterRelatorioAnaliseQuery(logId);
        var logFake = CriarAnaliseLogViaReflection(logId, "teste-status.log", erros, avisos, true);

        _repositoryMock
            .Setup(r => r.ObterTodosAsync())
            .ReturnsAsync(new List<AnaliseLog> { logFake });

        // Act
        var resultado = await _handler.Handle(query, CancellationToken.None);

        // Assert
        resultado.Should().NotBeNull();
        resultado.StatusSaude.Should().Contain(statusEsperado);
    }
}