using System;

namespace CriarPDF
{
    public interface IView
    {
        event EventHandler<SolicitacaoGerarPDFEventArgs> PrecisaGerarPDF;

        void ApresentarBoasVindas();
        void GerarPDF();
        void ExibirPDFGerado(string caminho);
    }
}
