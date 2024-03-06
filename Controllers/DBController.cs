
using SQLite;
using Tarea2._3GrabacionesAudios.Models;

namespace Tarea2._3GrabacionesAudios.Controllers
{
    public class DBController
    {

        //DATABASE CONFIGURATION VARIABLES

        private readonly static string dbFileName = "MyAppDB.db3";

        private readonly SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite |
                                                 SQLiteOpenFlags.Create |
                                                 SQLiteOpenFlags.SharedCache;

        private readonly string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbFileName);

        private SQLiteAsyncConnection connection;

        public DBController()
        {

        }

        private async Task Init()
        {
            if (connection is not null)
            {
                return;
            }
            connection = new SQLiteAsyncConnection(dbPath, flags);
            await connection.CreateTableAsync<Audio>();
        }


        //CREATE ==============================================================
        public async Task<int> Insert(Audio registro)
        {
            await Init();
            return await connection.InsertAsync(registro);
        }

        //READ ==============================================================
        public async Task<List<Audio>> SelectAll()
        {
            await Init();
            return await connection.Table<Audio>().ToListAsync();
        }


        public async Task<Audio> SelectById(int id)
        {
            await Init();
            return await connection.Table<Audio>().Where(col => col.Id == id).FirstOrDefaultAsync();
        }

        //UPDATE ==============================================================
        public async Task<int> Update(Audio registro)
        {
            await Init();
            return await connection.UpdateAsync(registro);
        }

        //DELETE ==============================================================
        public async Task<int> Delete(Audio registro)
        {
            await Init();
            return await connection.DeleteAsync(registro);
        }







    }
}
