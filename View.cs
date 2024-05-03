using System;
using System.IO;

namespace CriarPDF
{
    class View
    {
        public delegate void SolicitacaoGerarPDF(string texto, string tipoDeLetra, int tamanhoFonte, string cor);
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
        public void GerarPDF()
        {
            string texto = DigitarInformacoes();
            string caminho = SolicitarCaminhoPDF();
            (string tipoDeLetraEscolhido, int tamanhoFonte, string cor) = EscolherTipoDeLetraTamanhoECor();

            PrecisaGerarPDF?.Invoke(texto, tipoDeLetraEscolhido, tamanhoFonte, cor);
        }

        // Método para escolher o tipo de letra, tamanho da fonte e cor.
        private (string, int, string) EscolherTipoDeLetraTamanhoECor()
        {
            Console.WriteLine("Escolha o tipo de letra, o tamanho e a cor para o PDF:");
            Console.WriteLine("1. Verdana");
            Console.WriteLine("2. Arial");
            Console.WriteLine("3. Times New Roman");
            Console.Write("Tipo de letra (1-3): ");
            string tipoDeLetraEscolhido = Console.ReadLine();

            Console.Write("Tamanho da fonte (até 120): ");
            int tamanhoFonte;
            while (!int.TryParse(Console.ReadLine(), out tamanhoFonte) || tamanhoFonte <= 0 || tamanhoFonte > 120)
            {
                Console.WriteLine("Tamanho de fonte inválido. Por favor, insira um valor entre 1 e 120:");
            }

            Console.WriteLine("Escolha a cor para o PDF:");
            Console.WriteLine("1. Preto");
            Console.WriteLine("2. Cinza");
            Console.WriteLine("3. Verde");
            Console.WriteLine("4. Laranja");
            Console.WriteLine("5. Amarelo");
            Console.WriteLine("6. Rosa");
            Console.WriteLine("7. Vermelho");
            Console.WriteLine("8. Castanho");
            Console.WriteLine("9. Azul");
            Console.Write("Cor (1-9): ");
            string cor = Console.ReadLine();

            return (tipoDeLetraEscolhido, tamanhoFonte, cor);
        }
    }
}
