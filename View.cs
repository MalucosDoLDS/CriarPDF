using System;

namespace CriarPDF
{
    public class View : IView
    {
        public event EventHandler<SolicitacaoGerarPDFEventArgs>? PrecisaGerarPDF;

        public void ApresentarBoasVindas()
        {
            Console.WriteLine("Bem-vindo ao gerador de PDF!");
        }

        public void GerarPDF()
        {
            var elementos = new List<ElementoPDF>();
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("Deseja adicionar texto ou imagem? (T/I)");
                string escolha = Console.ReadLine().Trim().ToUpper();

                if (escolha == "T")
                {
                    string texto = DigitarInformacoes();
                    (string tipoDeLetraEscolhido, int tamanhoFonte, string cor) = EscolherTipoDeLetraTamanhoECor();
                    elementos.Add(new ElementoPDF(texto, tipoDeLetraEscolhido, tamanhoFonte, cor));
                }
                else if (escolha == "I")
                {
                    string caminhoImagem = SolicitarCaminhoImagem();
                    string tamanhoImagem = EscolherTamanhoImagem();
                    elementos.Add(new ElementoPDF(caminhoImagem, tamanhoImagem));
                }

                continuar = DesejaAdicionarMaisElementos();
            }

            string caminho = SolicitarCaminhoPDF();
            var args = new SolicitacaoGerarPDFEventArgs(elementos.ToArray(), caminho);
            OnPrecisaGerarPDF(args);
        }

        public void ExibirPDFGerado(string caminho)
        {
            Console.WriteLine($"PDF criado com sucesso em: {caminho}");
        }

        private bool DesejaAdicionarMaisElementos()
        {
            Console.WriteLine("Deseja adicionar mais algum elemento? (S/N)");
            string resposta = Console.ReadLine().ToUpper();
            return resposta == "S";
        }

        private string SolicitarCaminhoImagem()
        {
            Console.WriteLine("Por favor, insira o caminho da imagem:");
            Console.Write("> ");
            return Console.ReadLine();
        }

        private string EscolherTamanhoImagem()
        {
            Console.WriteLine("Escolha o tamanho da imagem:");
            Console.WriteLine("1. Pequena");
            Console.WriteLine("2. Média");
            Console.WriteLine("3. Grande");
            Console.Write("Tamanho da imagem (1-3): ");
            string tamanhoImagem = Console.ReadLine();
            return tamanhoImagem switch
            {
                "1" => "Pequena",
                "2" => "Média",
                "3" => "Grande",
                _ => "Média"
            };
        }

        private string SolicitarCaminhoPDF()
        {
            Console.WriteLine("Por favor, insira o caminho onde deseja salvar o arquivo PDF, escreva <nome>.pdf:");
            Console.Write("> ");
            return Console.ReadLine();
        }

        private string DigitarInformacoes()
        {
            Console.WriteLine("Digite o texto para o PDF:");
            return Console.ReadLine();
        }

        private (string, int, string) EscolherTipoDeLetraTamanhoECor()
        {
            Console.WriteLine("Escolha o tipo de letra para o PDF:");
            Console.WriteLine("1. Verdana");
            Console.WriteLine("2. Arial");
            Console.WriteLine("3. Times New Roman");
            Console.Write("Tipo de letra (1-3): ");
            string tipoDeLetraEscolhido = Console.ReadLine();

            Console.Write("Tamanho da fonte (até 120): ");
            int tamanhoFonte;
            while (!int.TryParse(Console.ReadLine(), out tamanhoFonte) || tamanhoFonte <= 0 || tamanhoFonte > 120)
            {
                Console.WriteLine("Tamanho de fonte inválido. Por favor, insira um valor entre 1 e 120:");
            }

            Console.WriteLine("Escolha a cor para o PDF:");
            Console.WriteLine("1. Preto");
            Console.WriteLine("2. Cinza");
            Console.WriteLine("3. Verde");
            Console.WriteLine("4. Laranja");
            Console.WriteLine("5. Amarelo");
            Console.WriteLine("6. Rosa");
            Console.WriteLine("7. Vermelho");
            Console.WriteLine("8. Castanho");
            Console.WriteLine("9. Azul");
            Console.Write("Cor (1-9): ");
            string cor = Console.ReadLine();

            return (tipoDeLetraEscolhido, tamanhoFonte, cor);
        }

        protected virtual void OnPrecisaGerarPDF(SolicitacaoGerarPDFEventArgs e)
        {
            PrecisaGerarPDF?.Invoke(this, e);
        }
    }
}



