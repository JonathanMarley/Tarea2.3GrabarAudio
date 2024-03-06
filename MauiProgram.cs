using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using Tarea2._3GrabacionesAudios.Views;

namespace Tarea2._3GrabacionesAudios
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton(AudioManager.Current);
            //builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ListadoAudios>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
