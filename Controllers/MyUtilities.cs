using System.Globalization;

namespace Tarea2._3GrabacionesAudios.Controllers
{
    public class MyUtilities : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch (value)
            {
                case byte[] fotoBytes:
                    byte[] bytes = value as byte[];
                    Stream stream = new MemoryStream(bytes);
                    return ImageSource.FromStream(() => stream);

                case string fotoPath:
                    return ImageSource.FromFile(fotoPath);



                default: return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }



    }
}
