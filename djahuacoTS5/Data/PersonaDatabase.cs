using SQLite;
using djahuacoTS5.Models;

namespace djahuacoTS5.Data
{
    public class PersonaDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public PersonaDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Persona>().Wait();
        }

        public Task<List<Persona>> ObtenerPersonasAsync()
        {
            return _database.Table<Persona>().ToListAsync();
        }

        public Task<int> GuardarPersonaAsync(Persona persona)
        {
            if (persona.Id != 0)
                return _database.UpdateAsync(persona);
            else
                return _database.InsertAsync(persona);
        }

        public Task<int> EliminarPersonaAsync(Persona persona)
        {
            return _database.DeleteAsync(persona);
        }
    }
}