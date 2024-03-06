using SQLite;

namespace Tarea2._3GrabacionesAudios.Models
{
    public class Audio
    {
        private List<string> invalidData = new List<string>();
        private byte[] audio;
        private string audioFilePath;
        private string nombre;
        private string descripcion;
        private string fecha;

        public Audio() { }



        public Audio(byte[] audio, string descripcion)
        {
            this.Audios = audio;
            this.Nombre = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.Descripcion = descripcion;
            this.AudioFilePath = Path.Combine(App.audiosDirectory, this.nombre);
            this.Fecha = DateTime.Today.ToString();
        }



        public List<string> GetDatosInvalidos()
        {
            return this.invalidData;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }




        [Column("Audio")]
        public byte[] Audios
        {
            get { return this.audio; }

            set
            {
                if (value != null && value.Length > 0)
                {
                    this.audio = value;

                }
                else
                {
                    this.invalidData.Add("No hay grabacion de audio");
                }
            }
        }




        [Column("Nombre")]
        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }





        [Column("Descripcion")]
        public string Descripcion
        {
            get { return this.descripcion; }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.descripcion = value;
                }
                else
                {
                    this.invalidData.Add("Descripcion vacía");
                }
            }
        }




        [Column("AudioFilePath")]
        public string AudioFilePath
        {
            get { return this.audioFilePath; }
            set { this.audioFilePath = value; }
        }





        [Column("Fecha")]
        public string Fecha
        {
            get { return this.fecha; }
            set { this.fecha = value; }
        }



    }
}
