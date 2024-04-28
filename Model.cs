using System;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace CriarPDF
{
    class Model
    {
        private bool estadoAtualDocumento;

        public Model()
        {
            estadoAtualDocumento = false;
        }

        public void GerarPDF(string texto, string caminho)
        {
            // Criando um novo documento PDF
            PdfDocument document = new PdfDocument();

            // Adicionando uma página ao documento
            PdfPage page = document.AddPage();

            // Obtendo um objeto XGraphics para desenhar na página
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Criando um objeto XFont para usar na página com o estilo Regular
            XFont font = new XFont("Verdana", 20); // Definindo a fonte como Verdana e o tamanho como 20

            // Desenhando o texto na página
            gfx.DrawString(texto, font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.Center);

            // Salvando o documento PDF no caminho fornecido
            document.Save(caminho);

            // Indicando que o PDF foi gerado com sucesso
            estadoAtualDocumento = true;

            Console.WriteLine($"PDF gerado e salvo em: {caminho}");
        }

        public void SolicitarPaginaAtual(ref string pagina)
        {
            // Simulação da obtenção da página atual
            pagina = "Conteúdo da página atual";
        }

        public bool DocumentoFoiGerado()
        {
            return estadoAtualDocumento;
        }
    }
}


