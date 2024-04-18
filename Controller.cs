using System;

namespace CriarPDF {
    class Controller {

        private Model model;
        private View view;

        // Eventos e delegados para comunicação 
        public delegate void ApresentacaoTexto();
        public event ApresentacaoTexto Apresentar;

        public delegate void Traducao(string Texto);
        public event Traducao Traduzir;

        public void IniciarPrograma() {

            // Inicialização dos componentes.
            view = new View();
            model = new Model();

            // Vinculação dos eventos entre os componentes

            //No Controller não seria preciso trabalhar
            //com eventos e delegados para os outros componentes,
            //porque o Controller tem sempre de conhecer o fluxo.
            //Contudo, mostra-se aqui como exemplo que
            //até no Controller se pode fazer isso para que a dependência
            //fique apenas neste método de inicialização, pelo que 
            //se a classe for muito extensa se manteve o acoplamento apenas
            //neste método.
            Traduzir += model.Traduzir;
            
            // Neste caso, estamos apenas a mostrar que se pode associar vários delegados
            // ao mesmo evento. Mas seria preferível ter dois distintos para assegurar a sequência
            // que aqui fica apenas implícita na ordem de associação
            Apresentar += view.ApresentarBoasVindas;
            Apresentar += view.ApresentarRotuloPrompt;
            
            // Notem que aqui estamos no Controller. A view não sabe que está a ser associado
            // o seu evento Enviar ao método Solicitar do model - um exemplo de desacoplamento
            view.PrecisoDaPaginaAtual += model.SolicitarPaginaAtual;
            // Outro exemplo de desacoplamento entre as classes Model e View
            // No diagrama de sequência, o Controller recebe o aviso de tradução pronta
            // e chama o MostrarTradução. Esta linha garante isso automaticamente.
            model.TraducaoPronta += view.MostrarTraducao;
            model.PaginaAtualMudou += view.PaginaAtualMudou;
            // Aqui um exemplo de desacoplamento entre o Model e o Controller. 
            // Embora o Controller saiba que será chamado, quem lança o evento é o Model, 
            // que não sabe a quem se destinárá: fica mais desacoplado.
            model.DocumentoTerminou += Encerrar;

            // Apresenta o programa
            Apresentar();

            // Inicia a tradução
            string texto = DigitarInformacoes();
            view.ApresentarRotuloTraducao();
            Traduzir(texto);
            while(model.EstadoAtualDocumento!=false)
            {
                TeclarQualquer();
                model.AvancarPagina();
            };
        }

        private void Encerrar()
        {
            view.MostrarMSGFinal();
            Environment.Exit(0);
        }

        private void TeclarQualquer()
        {
            Console.ReadKey();
        }

        public string DigitarInformacoes() {
            string texto = Console.ReadLine();
            return texto;
        }

    }

}
