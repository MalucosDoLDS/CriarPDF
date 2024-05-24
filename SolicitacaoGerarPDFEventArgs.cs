using System;

namespace CriarPDF
{
    // Classe que representa os argumentos do evento para solicitar a geração de um PDF
    public class SolicitacaoGerarPDFEventArgs : EventArgs
    {
        // Propriedade que armazena os elementos a serem incluídos no PDF
        public ElementoPDF[] Elementos { get; }

        // Propriedade que armazena o caminho onde o PDF será salvo
        public string Caminho { get; }

        // Construtor que inicializa a classe com os elementos e o caminho fornecidos
        public SolicitacaoGerarPDFEventArgs(ElementoPDF[] elementos, string caminho)
        {
            Elementos = elementos;  // Inicializa a propriedade Elementos com o valor fornecido
            Caminho = caminho;  // Inicializa a propriedade Caminho com o valor fornecido
        }
    }
}
