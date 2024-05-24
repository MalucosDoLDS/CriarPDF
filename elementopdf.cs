namespace CriarPDF
{
    // Enumeração para definir os tipos de elementos que podem ser adicionados ao PDF
    public enum TipoElemento
    {
        Texto,  // Elemento de texto
        Imagem  // Elemento de imagem
    }

    // Classe que representa um elemento a ser incluído no PDF
    public class ElementoPDF
    {
        public TipoElemento Tipo { get; }  // Propriedade para armazenar o tipo do elemento
        public string Texto { get; }  // Propriedade para armazenar o texto (apenas para elementos de texto)
        public string TipoDeLetra { get; }  // Propriedade para armazenar o tipo de letra (fonte) do texto
        public int TamanhoFonte { get; }  // Propriedade para armazenar o tamanho da fonte do texto
        public string Cor { get; }  // Propriedade para armazenar a cor do texto
        public string CaminhoImagem { get; }  // Propriedade para armazenar o caminho da imagem (apenas para elementos de imagem)
        public string TamanhoImagem { get; }  // Propriedade para armazenar o tamanho da imagem

        // Construtor para criar um elemento de texto
        public ElementoPDF(string texto, string tipoDeLetra, int tamanhoFonte, string cor)
        {
            Tipo = TipoElemento.Texto;  // Define o tipo como Texto
            Texto = texto;  // Inicializa a propriedade Texto
            TipoDeLetra = tipoDeLetra;  // Inicializa a propriedade TipoDeLetra
            TamanhoFonte = tamanhoFonte;  // Inicializa a propriedade TamanhoFonte
            Cor = cor;  // Inicializa a propriedade Cor
        }

        // Construtor para criar um elemento de imagem
        public ElementoPDF(string caminhoImagem, string tamanhoImagem)
        {
            Tipo = TipoElemento.Imagem;  // Define o tipo como Imagem
            CaminhoImagem = caminhoImagem;  // Inicializa a propriedade CaminhoImagem
            TamanhoImagem = tamanhoImagem;  // Inicializa a propriedade TamanhoImagem
        }
    }
}


