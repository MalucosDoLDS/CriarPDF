using System;

namespace CriarPDF
{
    class View
    {
        public delegate void SolicitacaoGerarPDF(string texto, string tipoDeLetra, int tamanhoFonte, string cor, string caminho);
        public event SolicitacaoGerarPDF? PrecisaGerarPDF;

        public void ApresentarBoasVindas()
        {
            Console.WriteLine("Bem-vindo ao gerador de PDF!");
        }

        public void GerarPDF()
        {
            string texto = DigitarInformacoes();
            (string tipoDeLetraEscolhido, int tamanhoFonte, string cor) = EscolherTipoDeLetraTamanhoECor();

            string caminho = SolicitarCaminhoPDF(); // Pergunta sobre o caminho no final

            PrecisaGerarPDF?.Invoke(texto, tipoDeLetraEscolhido, tamanhoFonte, cor, caminho);
        }

        public void ExibirPDFGerado(string caminho)
        {
            Console.WriteLine($"PDF criado com sucesso em: {caminho}");
        }

        private string SolicitarCaminhoPDF()
        {
            Console.WriteLine("Por favor, insira o caminho onde deseja salvar o arquivo PDF:");
            Console.Write("> ");
            return Console.ReadLine();
        }

        private string DigitarInformacoes()
        {
            Console.WriteLine("Digite o texto para o PDF:");
            return Console.ReadLine();
        }

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
