using SQLite;

namespace djahuacoTS5.Models
{
    public class Persona
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
    }
}