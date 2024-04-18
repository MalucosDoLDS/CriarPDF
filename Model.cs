namespace CriarPDF {
    class Model {

        private bool estadoAtualDocumento;
        
        public bool EstadoAtualDocumento { get => estadoAtualDocumento; }

        public delegate void EnvioTraducaoPronta(int nPaginas);
        public event EnvioTraducaoPronta TraducaoPronta;
        public delegate void NotificarOcorrencia();
        public event NotificarOcorrencia PaginaAtualMudou;
        public event NotificarOcorrencia DocumentoTerminou;

        public Model() {
            estadoAtualDocumento = false;
        }

        public void Traduzir(string texto) {
            // Traduzir o texto para chinês e guardá-lo
            // Notifica que a tradução está pronta;
            // Como é um evento, o model não sabe quem o receberá (isso é definido no Controller)
            int numPaginas = 0;
            estadoAtualDocumento = true;
            TraducaoPronta(numPaginas);
        }
        public void SolicitarPaginaAtual(ref string pagina) {
            // Fornece o texto traduzido da página atual
            // colocando-o em "pagina" porque foi passada com ref
            pagina = "Esta será a página atual da tradução";
        }
        public void AvancarPagina() {
            // Avança para a próxima página traduzida
            // estadoAtualDocumento regista sucesso (true) ou fim (false)
            if (estadoAtualDocumento)
                PaginaAtualMudou();
            else
                DocumentoTerminou();
        }
    }
}