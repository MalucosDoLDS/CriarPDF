using System;

namespace CriarPDF
{
    class View
    {
        public delegate void SolicitacaoGerarPDF(string texto);
        public event SolicitacaoGerarPDF? PrecisaGerarPDF;

        public View() { }
        public string SolicitarCaminhoPDF()
        {
            Console.WriteLine("Por favor, insira o caminho onde deseja salvar o arquivo PDF:");
            Console.Write("> ");
            string path = Console.ReadLine();

            // Verifica se a entrada é nula ou apenas espaços em branco
            while (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Caminho inválido. Por favor, digite um caminho válido para o arquivo PDF:");
                path = Console.ReadLine();
            }

            // Verifica se o caminho é um diretório existente ou tem caracteres inválidos
            if (Directory.Exists(path) || Path.GetInvalidPathChars().Any(path.Contains))
            {
                Console.WriteLine("Caminho fornecido é um diretório ou contém caracteres inválidos.");
                return null;  // Retorna nulo para indicar falha na validação
            }

            return path;
        }

        // Método para apresentar uma saudação inicial ao usuário.
        public void ApresentarBoasVindas()
        {
            Console.WriteLine("Bem-vindo ao gerador de PDF!");
        }

        // Método para solicitar ao usuário que digite o texto para incluir no PDF.
        public void SolicitarTextoParaPDF()
        {
            Console.WriteLine("Digite o texto para o PDF:");
            Console.Write("> ");
        }

        // Método para ler as informações inseridas pelo usuário e garantir que a entrada é válida.
        public string DigitarInformacoes()
        {
            Console.WriteLine("Digite o texto para o PDF:");
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Entrada inválida. Por favor, digite algum texto válido para o PDF:");
                input = Console.ReadLine();
            }
            return input;
        }

        // Método para exibir uma mensagem de sucesso após a criação do PDF.
        public void ExibirPDFGerado(string caminho)
        {
            Console.WriteLine($"PDF criado com sucesso em: {caminho}");
        }

        // Método para iniciar o processo de gerar um PDF a partir do texto fornecido.
        public void GerarPDF(string texto)
        {
            PrecisaGerarPDF?.Invoke(texto);
        }
    }
}
