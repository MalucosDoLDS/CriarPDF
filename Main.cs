using System;

namespace CriarPDF
{
    class QuartoChines
    {
        static void Main(string[] args)
        {
            View view = new View(); // Certifique-se de que a classe View está definida corretamente
            Model model = new Model(); // Certifique-se de que a classe Model está definida corretamente
            Controller controller = new Controller(view, model); // Passar as instâncias de View e Model para o construtor de Controller
            controller.Iniciar(); // Corrigido para chamar o método Iniciar, conforme definido na classe Controller
        }
       

    }
}