using System;
using System.IO;

namespace CriarPDF
{
    // Classe Controller que gerencia a interação entre a View e o Model
    public class Controller
    {
        private IView view; // Interface da View para a interação com o utilizador
        private IModel model; // Interface do Model para a lógica de geração do PDF

        // Construtor que inicializa a View e o Model, e subscreve o evento de geração de PDF
        public Controller(IView view, IModel model)
        {
            this.view = view;
            this.model = model;
            this.view.PrecisaGerarPDF += GerarPDF; // Subscreve o evento para tratar pedidos de geração de PDF
        }

        // Método para iniciar a interação com o utilizador
        public void Iniciar()
        {
            view.ApresentarBoasVindas(); // Apresenta uma mensagem de boas-vindas ao utilizador

            bool continuar = true; // Controla o loop de interação
            while (continuar)
            {
                view.GerarPDF(); // Solicita à View para iniciar o processo de geração do PDF
                continuar = PerguntarSeContinuar(); // Pergunta ao utilizador se deseja continuar
            }
        }

        // Método que trata o evento de geração de PDF
        private void GerarPDF(object sender, SolicitacaoGerarPDFEventArgs e)
        {
            try
            {
                string caminho = e.Caminho; // Obtém o caminho do ficheiro PDF

                if (ValidarCaminho(caminho)) // Valida o caminho fornecido
                {
                    bool success = model.GerarPDF(e.Elementos, caminho); // Solicita ao Model para gerar o PDF
                    if (success)
                    {
                        view.ExibirPDFGerado(caminho); // Exibe a mensagem de sucesso na View
                    }
                    else
                    {
                        Console.WriteLine("Falha ao gerar o PDF."); // Exibe uma mensagem de erro em caso de falha
                    }
                }
                else
                {
                    Console.WriteLine("Caminho inválido para o PDF: Certifique-se de que o caminho é absoluto e não contém caracteres inválidos."); // Mensagem de erro para caminho inválido
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro durante a geração do PDF: {ex.Message}"); // Exibe a mensagem de erro em caso de exceção
            }
        }

        // Método para validar o caminho do ficheiro PDF
        private bool ValidarCaminho(string caminho)
        {
            return Path.IsPathRooted(caminho) // Verifica se o caminho é absoluto
                && !Path.GetInvalidPathChars().Any(caminho.Contains) // Verifica se o caminho não contém caracteres inválidos
                && !Directory.Exists(caminho); // Verifica se o caminho não é um diretório existente
        }

        // Método que pergunta ao utilizador se deseja continuar a gerar mais PDFs
        private bool PerguntarSeContinuar()
        {
            Console.WriteLine("Deseja gerar outro documento PDF? (S/N)"); // Exibe a pergunta ao utilizador
            string resposta = Console.ReadLine().Trim().ToUpper(); // Lê a resposta do utilizador, removendo espaços e convertendo para maiúsculas
            return resposta == "S"; // Retorna true se a resposta for "S" (Sim)
        }
    }
}

