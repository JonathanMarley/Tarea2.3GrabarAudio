using Plugin.Maui.Audio;
using Tarea2._3GrabacionesAudios.Models;
using Microsoft.Maui.Controls.Compatibility;
using Tarea2._3GrabacionesAudios.Views;


namespace Tarea2._3GrabacionesAudios
{
    public partial class MainPage : ContentPage
    {
        private PermissionStatus permiso;
        readonly IAudioRecorder audioRecorder;
        private byte[] audioArray;

        public MainPage(IAudioManager audioManager)
        {
            InitializeComponent();
            this.audioRecorder = audioManager.CreateRecorder();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            permiso = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (permiso == PermissionStatus.Granted)
            {
                return;
            }
            else
            {
                permiso = await Permissions.RequestAsync<Permissions.Microphone>();
            }
        }

        private async void BtnGrabarAudio_Clicked(object sender, EventArgs args)
        {
            if (permiso == PermissionStatus.Granted)
            {
                if (!audioRecorder.IsRecording)
                {
                    await audioRecorder.StartAsync();
                    SetButtonRecordingStyle();

                }
                else
                {
                    var audio = await audioRecorder.StopAsync();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Stream st = audio.GetAudioStream();
                        await st.CopyToAsync(ms);
                        audioArray = ms.ToArray();
                    }

                    SetButtonRecordedStyle();
                    //var player = AudioManager.Current.CreatePlayer(recordedAudio.GetAudioStream());
                    //player.Play();
                }
            }
            else
            {
                await DisplayAlert("Grabar", "No se concedieron permisos para grabar", "Aceptar");
            }
        }


        private async void btnGuargar_Clicked(object sender, EventArgs args)
        {
            Audio audio = new Audio(
                audioArray,
            txtDescripcion.Text
                );

         // Guardado en SQLite y almacenamiento. (Si uno de los datos no es valido, se lanza el alert)
        if (!audio.GetDatosInvalidos().Any())
            {
                await App.db.Insert(audio);

                //Guardado del archivo de imagen en fisico.
                string path = Path.Combine(App.audiosDirectory, audio.Nombre);
                using (FileStream audioFile = File.OpenWrite(path))
                {
                    Stream st = new MemoryStream(audioArray);
                    await st.CopyToAsync(audioFile);
                }

                LimpiarCampos();
                await DisplayAlert("Guardar", "Datos guardados", "Aceptar");

            }
            else
            {
                string msj = string.Join("\n", audio.GetDatosInvalidos());
                await DisplayAlert("Datos Invalidos:", msj, "Acepar");
            }
        }

        private void SetButtonNormalStyle()
        {
            btnGrabarAudios.BackgroundColor = Colors.White;
            btnGrabarAudios.BorderColor = Colors.Green;
            btnGrabarAudios.ImageSource = new FileImageSource().File = "microfono.png";
        }
        private void SetButtonRecordingStyle()
        {
            btnGrabarAudios.BackgroundColor = Colors.White;
            btnGrabarAudios.BorderColor = Colors.Red;
            btnGrabarAudios.ImageSource = new FileImageSource().File = "detener.png";
            //btnBtnStartRecording.Text = "Detener";
        }

        private void SetButtonRecordedStyle()
        {
            btnGrabarAudios.BackgroundColor = Colors.DeepSkyBlue;
            btnGrabarAudios.BorderColor = Colors.Cyan;
            btnGrabarAudios.ImageSource = new FileImageSource().File = "comprobado.png";
            //btnBtnStartRecording.Text = "Detener";
        }

        private void btnLimpiarDatos_Clicked(object sender, EventArgs args)
        {
            LimpiarCampos();
        }

        private async void LimpiarCampos()
        {
            if (audioRecorder.IsRecording)
            {
                await audioRecorder.StopAsync();
            }
            SetButtonNormalStyle();
            audioArray = new byte[0];
            txtDescripcion.Text = string.Empty;
        }


        private async void btnlistas_Clicked(object sender, EventArgs args)
        {
            LimpiarCampos();
            await Navigation.PushAsync(new ListadoAudios());
        }





    }

}
