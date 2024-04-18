using System;

namespace CriarPDF {
    class View {

        public delegate void SolicitacaoPaginaAtual(ref string pagina);
        public event SolicitacaoPaginaAtual PrecisoDaPaginaAtual;

        public View() {
        }

        public void ApresentarBoasVindas() {
            Console.WriteLine("Bem vindo ao tradutor do Quarto Chinês!");
        }

        public void MostrarTraducao(int nPaginas) {
            //Obtém a página atual
            string pagina="";
            PrecisoDaPaginaAtual(ref pagina);
            //Colocá-la no ecrã.
            Console.WriteLine(pagina);
            Console.WriteLine("Prima qualquer tecla para continuar");
        }

        public void ApresentarRotuloPrompt() {
            Console.WriteLine("Digite o texto a traduzir");
            Console.Write("> ");
        }

        public void ApresentarRotuloTraducao() {
            Console.WriteLine("Texto traduzido:");
        }

        public void PaginaAtualMudou() {
            MostrarTraducao(1);
        }

        public void MostrarMSGFinal() { // ... 
        }
    }
}