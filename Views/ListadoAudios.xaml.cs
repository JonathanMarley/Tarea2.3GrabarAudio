using Plugin.Maui.Audio;
using System.Windows.Input;

namespace Tarea2._3GrabacionesAudios.Views;

public partial class ListadoAudios : ContentPage
{
    IAudioPlayer player;
    public ListadoAudios()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        viewListado.ItemsSource = await App.db.SelectAll();
    }




    public ICommand SwPlay => new Command<byte[]>(async (audio) =>
    {
        try
        {
            Stream stream = new MemoryStream(audio);
            player = AudioManager.Current.CreatePlayer(stream);
            player.Play();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Aceptar");
        }

    });
}