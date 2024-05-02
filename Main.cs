using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System;
using System.IO;

namespace CriarPDF
{
    class MainPDF
    {
        // Implementação personalizada do IFontResolver para gerenciar o uso de fontes
        public class CustomFontResolver : IFontResolver
        {
            private string defaultFontName;

            // Construtor para inicializar o nome da fonte padrão
            public CustomFontResolver(string defaultFontName)
            {
                this.defaultFontName = defaultFontName;
            }

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
                        return new FontResolverInfo(defaultFontName);
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
                    return File.ReadAllBytes(Path.Combine("fonts", defaultFontName + ".ttf"));
                }
            }
        }

        static void Main(string[] args)
        {
            // Certifique-se de passar um argumento para o construtor de CustomFontResolver
            // Aqui estou usando "Arial" como a fonte padrão, mas você pode escolher outra
            CustomFontResolver fontResolver = new CustomFontResolver("Arial");

            // Configure o resolver de fontes antes de qualquer outra operação
            GlobalFontSettings.FontResolver = fontResolver;

            View view = new View(); // Certifique-se de que a classe View está definida corretamente
            Model model = new Model(); // Certifique-se de que a classe Model está definida corretamente
            Controller controller = new Controller(view, model); // Passar as instâncias de View e Model para o construtor de Controller
            controller.Iniciar(); // Inicia o fluxo do programa através do Controller
        }
    }
}
