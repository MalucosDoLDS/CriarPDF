using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;

namespace CriarPDF
{
    class Controller
    {
        private View view;
        private Model model;

        public Controller(View view, Model model) // Adicionado um construtor que aceita instâncias de View e Model como argumentos
        {
            this.view = view;
            this.model = model;
            this.view.PrecisoDaPaginaAtual += HandleObterPaginaAtual;
            this.view.PrecisaGerarPDF += HandleGerarPDF;
        }

        public void Iniciar()
        {
            view.ApresentarBoasVindas();
            view.ApresentarRotuloPrompt();
            string texto = view.DigitarInformacoes();
            view.GerarPDF(texto);
        }

        private void HandleObterPaginaAtual(ref string traducao)
        {
            traducao = "Tradução do texto...";
            view.MostrarTraducao(ref traducao);
        }

        private void HandleGerarPDF(string texto)
        {
            string? caminho = view.SolicitarCaminhoPDF();
            if (caminho != null)
            {
                // Cria um novo documento PDF
                using (PdfDocument document = new PdfDocument())
                {
                    // Adiciona uma página ao documento
                    PdfPage page = document.AddPage();

                    // Obtém um objeto XGraphics para desenhar na página
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    // Define a fonte e o tamanho do texto
                    XFont font = new XFont("Arial", 12);

                    // Desenha o texto na página
                    gfx.DrawString(texto, font, XBrushes.Black,
                        new XRect(10, 10, page.Width, page.Height),
                        XStringFormats.TopLeft);

                    // Salva o documento PDF no caminho fornecido
                    document.Save(caminho);
                }

                // Informa que o PDF foi gerado
                model.GerarPDF(texto, caminho);
                view.ExibirPDFGerado(caminho);
            }
            else
            {
                Console.WriteLine("Caminho inválido para o PDF.");
            }
        }
    }

}

