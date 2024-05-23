namespace CriarPDF
{
    public class ElementoPDF
    {
        public TipoElemento Tipo { get; set; }
        public string Conteudo { get; set; }
        public string TipoDeLetra { get; set; }
        public int TamanhoFonte { get; set; }
        public string Cor { get; set; }
    }

    public enum TipoElemento
    {
        Texto,
        Imagem
    }
}
