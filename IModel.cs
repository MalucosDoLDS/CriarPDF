namespace CriarPDF
{
    public interface IModel
    {
        bool GerarPDF(ElementoPDF[] elementos, string caminho);
        bool DocumentoFoiGerado();
    }
}
