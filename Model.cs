using System;
using System.Collections.Generic;
using System.Drawing;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace CriarPDF
{
    public class Model : IModel
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
                double currentYPosition = rect.Top;

                foreach (var elemento in elementos)
                {
                    if (elemento.Tipo == TipoElemento.Texto)
                    {
                        XFont font = new XFont(elemento.TipoDeLetra, elemento.TamanhoFonte);
                        var tf = new XTextFormatter(gfx)
                        {
                            Alignment = XParagraphAlignment.Left
                        };

                        var lines = BreakTextIntoLines(elemento.Texto, font, gfx, rect.Width);
                        foreach (var line in lines)
                        {
                            double height = gfx.MeasureString(line, font).Height;

                            if (currentYPosition + height > rect.Bottom)
                            {
                                AddPageFooter(gfx, document.PageCount);
                                currentPage = AddPage(document);
                                gfx = XGraphics.FromPdfPage(currentPage);
                                currentYPosition = rect.Top;
                            }

                            tf.DrawString(line, font, GetXBrush(elemento.Cor), new XRect(rect.Left, currentYPosition, rect.Width, height));
                            currentYPosition += height;
                        }
                    
                }
                    else if (elemento.Tipo == TipoElemento.Imagem)
                    {
                        XImage image = XImage.FromFile(elemento.CaminhoImagem);
                        SizeF imageSize = GetImageSize(image, elemento.TamanhoImagem, currentPage);

                        if (currentYPosition + imageSize.Height > rect.Bottom)
                        {
                            AddPageFooter(gfx, document.PageCount);
                            currentPage = AddPage(document);
                            gfx = XGraphics.FromPdfPage(currentPage);
                            currentYPosition = rect.Top;
                        }

                        double centerX = (currentPage.Width - imageSize.Width) / 2;
                        gfx.DrawImage(image, centerX, currentYPosition, imageSize.Width, imageSize.Height);
                        currentYPosition += imageSize.Height;
                    }
                }

                AddPageFooter(gfx, document.PageCount); // Add footer for the last page

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
            return document.AddPage();
        }

        private SizeF GetImageSize(XImage image, string tamanhoImagem, PdfPage page)
        {
            float maxWidth = (float)page.Width - 80;
            float maxHeight = (float)page.Height - 80;

            return tamanhoImagem switch
            {
                "Pequena" => new SizeF(maxWidth / 4, maxHeight / 4),
                "Média" => new SizeF(maxWidth / 2, maxHeight / 2),
                "Grande" => new SizeF(maxWidth, maxHeight),
                _ => new SizeF(maxWidth / 2, maxHeight / 2)
            };
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
                _ => XBrushes.Black
            };
        }

        public bool DocumentoFoiGerado()
        {
            return estadoAtualDocumento;
        }

        private List<string> BreakTextIntoLines(string text, XFont font, XGraphics gfx, double maxWidth)
        {
            var words = text.Split(' ');
            var lines = new List<string>();
            var currentLine = string.Empty;

            foreach (var word in words)
            {
                var testLine = string.IsNullOrEmpty(currentLine) ? word : $"{currentLine} {word}";
                var size = gfx.MeasureString(testLine, font);

                if (size.Width < maxWidth)
                {
                    currentLine = testLine;
                }
                else
                {
                    lines.Add(currentLine);
                    currentLine = word;
                }
            }

            if (!string.IsNullOrEmpty(currentLine))
            {
                lines.Add(currentLine);
            }

            return lines;
        }

        private void AddPageFooter(XGraphics gfx, int pageNumber)
        {
            XFont footerFont = new XFont("Arial", 10);
            string footerText = $"Page {pageNumber}";
            XRect footerRect = new XRect(0, gfx.PageSize.Height - 30, gfx.PageSize.Width, 20);

            gfx.DrawString(footerText, footerFont, XBrushes.Black, footerRect, XStringFormats.Center);
        }
    }
}
