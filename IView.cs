namespace CriarPDF
{
    public interface IView
    {
        event EventHandler<SolicitacaoGerarPDFEventArgs>? PrecisaGerarPDF;

        void ApresentarBoasVindas();
        void GerarPDF();
        void ExibirPDFGerado(string caminho);
    }

    public class SolicitacaoGerarPDFEventArgs : EventArgs
    {
        public string Texto { get; set; }
        public string TipoDeLetra { get; set; }
        public int TamanhoFonte { get; set; }
        public string Cor { get; set; }
        public string Caminho { get; set; }

        public SolicitacaoGerarPDFEventArgs(string texto, string tipoDeLetra, int tamanhoFonte, string cor, string caminho)
        {
            Texto = texto;
            TipoDeLetra = tipoDeLetra;
            TamanhoFonte = tamanhoFonte;
            Cor = cor;
            Caminho = caminho;
        }
    }
}
