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

        // Método para gerar um PDF com o texto fornecido, utilizando o tipo de letra escolhido, e salvar no caminho especificado.
        public bool GerarPDF(string texto, string caminho, string tipoDeLetra)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont font;
                switch (tipoDeLetra)
                {
                    case "1":
                        font = new XFont("Verdana", 20);
                        break;
                    case "2":
                        font = new XFont("Arial", 20);
                        break;
                    case "3":
                        font = new XFont("Times New Roman", 20);
                        break;
                    default:
                        font = new XFont("Arial", 20); // Padrão para Arial se a escolha não for reconhecida
                        break;
                }

                gfx.DrawString(texto, font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);
                document.Save(caminho);
                estadoAtualDocumento = true;
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Falha ao gerar o PDF: {ex.Message}");
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

