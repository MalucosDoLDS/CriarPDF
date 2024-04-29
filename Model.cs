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
            estadoAtualDocumento = false;  // Inicializa o estado do documento como não gerado.
        }

        // Método para gerar um PDF com o texto fornecido e salvar no caminho especificado.
        public bool GerarPDF(string texto, string caminho)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Combinação manual de estilos de fonte para simular "BoldItalic"
                XFont font = new XFont("Verdana", 20);

                gfx.DrawString(texto, font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);
                document.Save(caminho);
                estadoAtualDocumento = true;
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to generate PDF: {ex.Message}");
                return false;
            }
        }

        // Método para obter uma simulação de conteúdo de página, útil para testes.
        public void SolicitarPaginaAtual(ref string pagina)
        {
            pagina = "Conteúdo da página atual";
        }

        // Método para verificar se um documento PDF foi gerado.
        public bool DocumentoFoiGerado()
        {
            return estadoAtualDocumento;
        }
    }
}
