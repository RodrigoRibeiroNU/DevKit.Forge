namespace DevKit.Forge.Domain.Entities;

public class AnaliseLog
{
    // Exigido pelo EF Core para materialização de entidades persistidas.
    protected AnaliseLog() { }

    public AnaliseLog(string nomeArquivo)
    {
        Id = Guid.NewGuid();
        NomeArquivo = nomeArquivo;
        DataProcessamento = DateTime.Now;
        Sucesso = false;
    }

    public Guid Id { get; private set; }
    public string NomeArquivo { get; private set; } = string.Empty;
    public DateTime DataProcessamento { get; private set; }
    public bool Sucesso { get; private set; }
    
    public int QtdErros { get; private set; }
    public int QtdAvisos { get; private set; }

    /// <summary>
    /// Classifica o conteúdo do arquivo com base em padrões de severidade reconhecidos em logs de aplicação.
    /// </summary>
    public void ProcessarConteudo(string conteudo)
    {
        if (string.IsNullOrWhiteSpace(conteudo))
        {
            // Conteúdo ausente não indica falha de processamento nem presença de erros.
            Sucesso = true;
            return;
        }

        var linhas = conteudo.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        int erros = 0;
        int avisos = 0;

        foreach (var linha in linhas)
        {
            var linhaMaiuscula = linha.ToUpper();

            if (EhLinhaDeErro(linhaMaiuscula))
            {
                erros++;
                continue;
            }

            if (EhLinhaDeAviso(linhaMaiuscula))
            {
                avisos++;
            }
        }

        QtdErros = erros;
        QtdAvisos = avisos;
        // Sucesso registra a conclusão da análise, independentemente das contagens encontradas.
        Sucesso = true;
    }

    private static bool EhLinhaDeErro(string linhaMaiuscula) =>
        linhaMaiuscula.Contains("[ERROR]") || linhaMaiuscula.Contains("EXCEPTION");

    private static bool EhLinhaDeAviso(string linhaMaiuscula) =>
        linhaMaiuscula.Contains("[WARNING]") || linhaMaiuscula.Contains("[WARN]");
}