namespace CriarPDF
{
    public enum TipoElemento
    {
        Texto,
        Imagem
    }

    public class ElementoPDF
    {
        public TipoElemento Tipo { get; }
        public string Texto { get; }
        public string TipoDeLetra { get; }
        public int TamanhoFonte { get; }
        public string Cor { get; }
        public string CaminhoImagem { get; }
        public string TamanhoImagem { get; }

        public ElementoPDF(string texto, string tipoDeLetra, int tamanhoFonte, string cor)
        {
            Tipo = TipoElemento.Texto;
            Texto = texto;
            TipoDeLetra = tipoDeLetra;
            TamanhoFonte = tamanhoFonte;
            Cor = cor;
        }

        public ElementoPDF(string caminhoImagem, string tamanhoImagem)
        {
            Tipo = TipoElemento.Imagem;
            CaminhoImagem = caminhoImagem;
            TamanhoImagem = tamanhoImagem;
        }
    }
}

