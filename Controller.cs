using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace CriarPDF
{
    class Controller
    {
        private View view;
        private Model model;

        public Controller(View view, Model model)
        {
            this.view = view;
            this.model = model;
        }

        public void Iniciar()
        {
            view.ApresentarBoasVindas();
            try
            {
                string texto = view.DigitarInformacoes();
                string caminho = view.SolicitarCaminhoPDF();

                if (ValidarCaminho(caminho))
                {
                    bool success = model.GerarPDF(texto, caminho);
                    if (success)
                    {
                        view.ExibirPDFGerado(caminho);
                    }
                    else
                    {
                        Console.WriteLine("Falha ao gerar o PDF.");
                    }
                }
                else
                {
                    Console.WriteLine("Caminho inválido para o PDF: Certifique-se de que o caminho é absoluto e não contém caracteres inválidos.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a geração do PDF: {ex.Message}");
            }
        }

        private bool ValidarCaminho(string caminho)
        {
            return Path.IsPathRooted(caminho) && !Path.GetInvalidPathChars().Any(caminho.Contains) && !Directory.Exists(caminho);
        }
    }
}