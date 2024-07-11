using System.Globalization;
using System.Net;
using System.Text;
using Microsoft.Maui.Graphics;
using SkiaSharp;
using Microsoft.Maui.Graphics;
using Svg.Skia;

namespace Ejercicio2_1.Converters
{
    public class ImageSvgToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var uriString = (string)value;
            Uri uri = new Uri(uriString);

            if (uri.AbsolutePath.ToLowerInvariant().EndsWith(".svg"))
            {
                using (var webClient = new WebClient())
                {
                    var svgString = webClient.DownloadString(uri);
                    var svgDocument = new SKSvg();
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(svgString)))
                    {
                        svgDocument.Load(stream);
                    }

                    // Crear un nuevo bitmap y dibujar la imagen SVG en él
                    var width = (int)svgDocument.Picture.CullRect.Width;
                    var height = (int)svgDocument.Picture.CullRect.Height;
                    var bitmap = new SKBitmap(width, height);
                    using (var canvas = new SKCanvas(bitmap))
                    {
                        canvas.Clear(SKColors.Transparent);
                        canvas.DrawPicture(svgDocument.Picture);
                    }

                    // Convertir el bitmap a un formato compatible con .NET MAUI (por ejemplo, PNG)
                    using (var image = SKImage.FromBitmap(bitmap))
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        var bytes = data.ToArray();
                        // Guardar bytes en un archivo temporal o almacenarlos en caché según sea necesario
                        // Devolver el camino al archivo o los bytes para mostrar la imagen en .NET MAUI
                        return bytes;
                    }
                }
            }
            else
            {
                return ImageSource.FromUri(uri);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
