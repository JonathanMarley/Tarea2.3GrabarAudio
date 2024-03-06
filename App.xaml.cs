using Plugin.Maui.Audio;
using Tarea2._3GrabacionesAudios.Controllers;
using Tarea2._3GrabacionesAudios.Views;
using System.IO;

namespace Tarea2._3GrabacionesAudios
{
    public partial class App : Application {
        public static readonly DBController db = new DBController();
        public static readonly string audiosDirectory = Path.Combine(FileSystem.AppDataDirectory, "Audios");

        public App() {
            // Código para acceder al archivo
            InitializeComponent();
            
           
            //valida existencia del directorio de los audios, si no existe, lo crea.
            if (!Directory.Exists(audiosDirectory))
            {
                Directory.CreateDirectory(audiosDirectory);
            }

            MainPage = new NavigationPage(new MainPage(AudioManager.Current));
        }
    }
}
