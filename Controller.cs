using System;
using System.IO;

namespace CriarPDF
{
    public class Controller
    {
        private IView view;
        private IModel model;

        public Controller(IView view, IModel model)
        {
            this.view = view;
            this.model = model;
            this.view.PrecisaGerarPDF += GerarPDF;
        }

        public void Iniciar()
        {
            view.ApresentarBoasVindas();

            bool continuar = true;
            while (continuar)
            {
                view.GerarPDF();
                continuar = PerguntarSeContinuar();
            }
        }

        private void GerarPDF(object sender, SolicitacaoGerarPDFEventArgs e)
        {
            try
            {
                string caminho = e.Caminho;

                if (ValidarCaminho(caminho))
                {
                    bool success = model.GerarPDF(e.Elementos, caminho);
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

        private bool PerguntarSeContinuar()
        {
            Console.WriteLine("Deseja gerar outro documento PDF? (S/N)");
            string resposta = Console.ReadLine().Trim().ToUpper();
            return resposta == "S";
        }
    }
}
