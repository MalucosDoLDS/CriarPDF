using System;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace CriarPDF
{
    class Model : IModel
    {
        private bool estadoAtualDocumento;

        public Model()
        {
            estadoAtualDocumento = false;  // Inicializa o estado do documento como não gerado.
        }

        // Método para gerar um PDF com o texto fornecido e salvar no caminho especificado, com o tipo de letra, tamanho da fonte e cor escolhidos.
        public bool GerarPDF(string texto, string caminho, string tipoDeLetra, int tamanhoFonte, string cor)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                PdfPage currentPage = AddPage(document); // Adiciona a primeira página
                XGraphics gfx = XGraphics.FromPdfPage(currentPage);

                // Combinação manual de estilos de fonte para simular "BoldItalic"
                XFont font;
                switch (tipoDeLetra)
                {
                    case "1":
                        font = new XFont("Verdana", tamanhoFonte);
                        break;
                    case "2":
                        font = new XFont("Arial", tamanhoFonte);
                        break;
                    case "3":
                        font = new XFont("Times New Roman", tamanhoFonte);
                        break;
                    default:
                        font = new XFont("Verdana", tamanhoFonte);
                        break;
                }

                // Define as margens da página
                XRect rect = new XRect(40, 40, currentPage.Width - 80, currentPage.Height - 80);

                // Divide o texto em partes para caber nas páginas
                int index = 0;
                while (index < texto.Length)
                {
                    string line = GetNextLine(texto, index, font, rect.Width);
                    if (gfx.MeasureString(line, font).Height + rect.Top > currentPage.Height - 80) // Verifica se a linha cabe na página atual
                    {
                        currentPage = AddPage(document); // Se não couber, adiciona uma nova página
                        gfx = XGraphics.FromPdfPage(currentPage);
                        rect = new XRect(40, 40, currentPage.Width - 80, currentPage.Height - 80);
                    }

                    gfx.DrawString(line, font, GetXBrush(cor), rect, XStringFormats.TopLeft);
                    rect = new XRect(rect.Left, rect.Top + font.Height, rect.Width, rect.Height);
                    index += line.Length;
                }

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

        // Adiciona uma nova página ao documento
        private PdfPage AddPage(PdfDocument document)
        {
            PdfPage page = document.AddPage();
            return page;
        }

        // Método auxiliar para obter a próxima linha de texto que cabe na largura especificada
        private string GetNextLine(string text, int startIndex, XFont font, double maxWidth)
        {
            string line = "";
            int index = startIndex;
            while (index < text.Length && GetStringSize(line + text[index], font).Width < maxWidth)
            {
                line += text[index];
                index++;
            }
            return line.TrimEnd();
        }

        // Método auxiliar para obter a largura do texto
        private XSize GetStringSize(string text, XFont font)
        {
            using (XGraphics gfx = XGraphics.CreateMeasureContext(new XSize(1000, 1000), XGraphicsUnit.Point, XPageDirection.Downwards))
            {
                XSize size = gfx.MeasureString(text, font);
                return size;
            }
        }

        // Método para verificar se um documento PDF foi gerado.
        public bool DocumentoFoiGerado()
        {
            return estadoAtualDocumento;
        }

        // Método para obter a cor do texto
        private XBrush GetXBrush(string cor)
        {
            switch (cor)
            {
                case "1":
                    return XBrushes.Black;
                case "2":
                    return XBrushes.Gray;
                case "3":
                    return XBrushes.Green;
                case "4":
                    return XBrushes.Orange;
                case "5":
                    return XBrushes.Yellow;
                case "6":
                    return XBrushes.Pink;
                case "7":
                    return XBrushes.Red;
                case "8":
                    return XBrushes.Brown;
                case "9":
                    return XBrushes.Blue;
                default:
                    return XBrushes.Black;
            }
        }
    }
}
