using System;

namespace CriarPDF
{
    public class SolicitacaoGerarPDFEventArgs : EventArgs
    {
        public ElementoPDF[] Elementos { get; }
        public string Caminho { get; }

        public SolicitacaoGerarPDFEventArgs(ElementoPDF[] elementos, string caminho)
        {
            Elementos = elementos;
            Caminho = caminho;
        }
    }
}
