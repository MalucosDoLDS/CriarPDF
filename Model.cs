using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace CriarPDF
{
    class Model : IModel
    {
        private bool estadoAtualDocumento;

        public Model()
        {
            estadoAtualDocumento = false;
        }

        public bool GerarPDF(ElementoPDF[] elementos, string caminho)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                PdfPage currentPage = AddPage(document);
                XGraphics gfx = XGraphics.FromPdfPage(currentPage);
                XRect rect = new XRect(40, 40, currentPage.Width - 80, currentPage.Height - 80);

                foreach (var elemento in elementos)
                {
                    if (elemento.Tipo == TipoElemento.Texto)
                    {
                        string texto = elemento.Conteudo;
                        XFont font = new XFont(elemento.TipoDeLetra, elemento.TamanhoFonte);
                        XBrush brush = GetXBrush(elemento.Cor);

                        int index = 0;
                        while (index < texto.Length)
                        {
                            string line = GetNextLine(texto, index, font, rect.Width);
                            if (gfx.MeasureString(line, font).Height + rect.Top > currentPage.Height - 80)
                            {
                                currentPage = AddPage(document);
                                gfx = XGraphics.FromPdfPage(currentPage);
                                rect = new XRect(40, 40, currentPage.Width - 80, currentPage.Height - 80);
                            }

                            gfx.DrawString(line, font, brush, rect, XStringFormats.TopLeft);
                            rect = new XRect(rect.Left, rect.Top + font.Height, rect.Width, rect.Height);
                            index += line.Length;
                        }
                    }
                    else if (elemento.Tipo == TipoElemento.Imagem)
                    {
                        XImage image = XImage.FromFile(elemento.Conteudo);
                        gfx.DrawImage(image, rect.Left, rect.Top, rect.Width, rect.Height);
                        rect = new XRect(rect.Left, rect.Top + image.PixelHeight, rect.Width, rect.Height);
                    }
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

        private PdfPage AddPage(PdfDocument document)
        {
            PdfPage page = document.AddPage();
            return page;
        }

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

        private XSize GetStringSize(string text, XFont font)
        {
            using (XGraphics gfx = XGraphics.CreateMeasureContext(new XSize(1000, 1000), XGraphicsUnit.Point, XPageDirection.Downwards))
            {
                XSize size = gfx.MeasureString(text, font);
                return size;
            }
        }

        public bool DocumentoFoiGerado()
        {
            return estadoAtualDocumento;
        }

        private XBrush GetXBrush(string cor)
        {
            return cor switch
            {
                "1" => XBrushes.Black,
                "2" => XBrushes.Gray,
                "3" => XBrushes.Green,
                "4" => XBrushes.Orange,
                "5" => XBrushes.Yellow,
                "6" => XBrushes.Pink,
                "7" => XBrushes.Red,
                "8" => XBrushes.Brown,
                "9" => XBrushes.Blue,
                _ => XBrushes.Black,
            };
        }
    }
}
