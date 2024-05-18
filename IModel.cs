namespace CriarPDF
{
    public interface IModel
    {
        bool GerarPDF(string texto, string caminho, string tipoDeLetra, int tamanhoFonte, string cor);
        bool DocumentoFoiGerado();
    }
}
