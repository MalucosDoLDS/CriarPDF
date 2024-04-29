using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System;
using System.IO;

namespace CriarPDF
{
    class MainPDF
    {
        // O inicializador estático configura o resolver de fontes antes de qualquer outra operação
        static MainPDF()
        {
            try
            {
                GlobalFontSettings.FontResolver = new CustomFontResolver();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao configurar o resolver de fontes: {ex.Message}");
                Environment.Exit(1); // Termina a execução se não for possível configurar o resolver
            }
        }

        static void Main(string[] args)
        {
            View view = new View(); // Certifique-se de que a classe View está definida corretamente
            Model model = new Model(); // Certifique-se de que a classe Model está definida corretamente
            Controller controller = new Controller(view, model); // Passar as instâncias de View e Model para o construtor de Controller
            controller.Iniciar(); // Inicia o fluxo do programa através do Controller
        }
    }

    // Implementação personalizada do IFontResolver para gerenciar o uso de fontes
    public class CustomFontResolver : IFontResolver
    {
        public string DefaultFontName => "Verdana";

        // Resolve o tipo de letra com base no nome da família, e indicadores de negrito e itálico
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            string suffix = "";
            if (isBold) suffix += "b";
            if (isItalic) suffix += "i";

            // Gera o nome da fonte a ser usada
            string fontName = familyName.ToLower() + suffix;
            switch (fontName)
            {
                case "verdanab":
                    return new FontResolverInfo("Verdana#b");
                case "verdanai":
                    return new FontResolverInfo("Verdana#i");
                case "verdanabi":
                    return new FontResolverInfo("Verdana#bi");
                default:
                    return new FontResolverInfo(DefaultFontName);
            }
        }

        // Carrega os dados da fonte a partir do sistema de arquivos
        public byte[] GetFont(string faceName)
        {
            string path = Path.Combine("fonts", faceName.Replace("#", "") + ".ttf");
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            else
            {
                Console.Error.WriteLine($"Font file not found: {path}");
                throw new FileNotFoundException($"The font file '{path}' does not exist.");
            }
        }
    }
}
