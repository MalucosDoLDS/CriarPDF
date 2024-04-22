using System;

namespace CriarPDF
{
    class View
    {
        public delegate void SolicitacaoPaginaAtual(ref string pagina);
        public event SolicitacaoPaginaAtual? PrecisoDaPaginaAtual;

        public delegate void SolicitacaoGerarPDF(string texto);
        public event SolicitacaoGerarPDF? PrecisaGerarPDF;

        public View() { }

        public void ApresentarBoasVindas()
        {
            Console.WriteLine("Bem-vindo ao tradutor do Quarto Chinês!");
        }

        public void ApresentarRotuloPrompt()
        {
            Console.WriteLine("Digite o texto a traduzir:");
            Console.Write("> ");
        }

        public string DigitarInformacoes()
        {
            return Console.ReadLine();
        }

        public string? SolicitarCaminhoPDF()
        {
            Console.WriteLine("Por favor, insira o caminho onde deseja salvar o arquivo PDF:");
            Console.Write("> ");
            string path = Console.ReadLine();
            return path; // Retorna null se o caminho for nulo
        }

        public void ExibirPDFGerado(string path)
        {
            Console.WriteLine($"PDF criado com sucesso em: {path}");
        }

        // Método para iniciar o processo de gerar PDF
        public void GerarPDF(string texto)
        {
            PrecisaGerarPDF?.Invoke(texto);
        }

        // Método para obter a página atual para mostrar
        public void MostrarTraducao(ref string traducao)
        {
            if (traducao != null)
            {
                PrecisoDaPaginaAtual?.Invoke(ref traducao);
            }
        }
    }
}