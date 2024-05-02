using PdfSharp.Fonts;
using System;
using System.IO;

namespace CriarPDF
{
    // Implementação personalizada do IFontResolver para gerenciar o uso de fontes
    public class CustomFontResolver : IFontResolver
    {
        // Atributo para armazenar o nome da fonte padrão
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
                // Retorna uma fonte padrão ou lança uma exceção, dependendo dos requisitos do aplicativo
                // Exemplo de retorno de uma fonte padrão:
                return File.ReadAllBytes(Path.Combine("fonts", defaultFontName + ".ttf"));
                // Exemplo de lançamento de exceção:
                // throw new FileNotFoundException($"The font file '{path}' does not exist.");
            }
        }
    }
}
