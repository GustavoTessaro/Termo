using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Net;
class Program
{

    private static readonly HttpClient client = new HttpClient();

    public static async Task<string> ObterPalavraAleatoria()
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("User-Agent", "JogoDaForca");

            while (true) // Repete até encontrar a palavra certa
            {
                try
                {
                    string url = "https://api.dicionario-aberto.net/random";
                    string responseBody = await client.GetStringAsync(url);

                    using (JsonDocument doc = JsonDocument.Parse(responseBody))
                    {
                        string palavra = doc.RootElement.GetProperty("word").GetString();

                        // Remove acentos antes de contar (opcional, mas recomendado)
                        string palavraLimpa = RemoverAcentos(palavra);

                        if (palavraLimpa.Length == 5)
                        {
                            return palavraLimpa.ToUpper();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro na requisição: {ex.Message}");
                    return "TERMO"; // Palavra padrão de 5 letras em caso de erro
                }
            }
        }
    }

    public static string RemoverAcentos(string palavra)
    {
        var normalizada = palavra.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalizada)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    public static void MostrarRegras()
    {
        Console.WriteLine("Regras:");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("O jogo possui uma lista de palavras de 5 letras previamente cadastradas.");
        Console.WriteLine("Uma palavra é sorteada aleatoriamente no início da partida.");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("Após cada tentativa, o jogo exibe a palavra digitada novamente, destacando cada letra com uma cor diferente:");
        Console.WriteLine("\n     Vermelho escuro: letra inexistente na palavra.");
        Console.WriteLine("     Amarelo escuro: a letra existe, mas está na posição errada.");
        Console.WriteLine("     Verde escuro: letra correta na posição correta.");
        Console.WriteLine("\n-------------------------------");
        Console.WriteLine("O jogador tem 5 tentativas para adivinhar a palavra correta.");
        Console.WriteLine("-------------------------------");
    }

    public static void ExibirPalavraComCores(string tentativa, string palavra)
    {
        for (int i = 0; i < tentativa.Length; i++)
        {
            char letra = tentativa[i];
            if (letra == palavra[i])
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (palavra.Contains(letra))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(letra);
        }
        Console.ResetColor();
    }

    public static bool JogarNovamente()
    {
        while (true)
        {

            Console.Write("\nDeseja jogar novamente? (S/N): ");
            char resposta = char.ToUpper(Console.ReadKey().KeyChar);

            if (resposta == 'S')
            {
                Console.Clear();
                return true;
            }
            else if (resposta == 'N')
            {
                Console.WriteLine("\nObrigado por jogar! Até a próxima!");
                return false;
            }
            else
            {
                Console.WriteLine("\nResposta inválida. Por favor, digite S para sim ou N para não.");
                continue;
            }

        }

    }
    static void Main(string[] args)
    {

        Console.WriteLine("-------------------------------");
        Console.WriteLine("Bem vindo ao jogo do Termo!");
        Console.WriteLine("-------------------------------");

        MostrarRegras();

        Console.WriteLine("\nVamos Começar!");
        Console.Write("\nPrecione Enter para continuar.");
        Console.ReadLine();
        Console.Clear();

        bool jogarNovamente = true;

        while (jogarNovamente == true)
        {
            Console.WriteLine("Sorteando palavra AGUARDE...");
            string palavra = ObterPalavraAleatoria().Result;
            palavra = RemoverAcentos(palavra);
            Console.Clear();

            bool acertou = false;
            string tentativa = "";

            for (int i = 0; i < 5; i++)
            {

                do
                {
                    Console.Write($"\nDigite uma palavra de 5 letras ({5 - i} tentativas restantes): ");
                    tentativa = Console.ReadLine().ToUpper();

                    if (tentativa.Length != 5)
                    {
                        Console.WriteLine("A palavra deve conter exatamente 5 letras. Tente novamente.");
                    }

                } while (tentativa.Length != 5);

                ExibirPalavraComCores(tentativa, palavra);

                if (tentativa == palavra)
                {
                    Console.WriteLine("\nParabéns! Você acertou a palavra!");
                    acertou = true;
                    break;
                }

            }

            if (acertou == false)
            {
                Console.WriteLine($"\nGame Over! A palavra correta era: {palavra}");
            }

            Console.WriteLine($"\nPara pesquisar o significado da palavra '{palavra}', acesse:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"https://dicio.com.br/{palavra.ToLower()}/");
            Console.ResetColor();

            jogarNovamente = JogarNovamente();

        }

    }
}