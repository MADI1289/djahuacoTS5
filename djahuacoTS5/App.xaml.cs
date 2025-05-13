using djahuacoTS5.Data;

namespace djahuacoTS5
{
    public partial class App : Application
    {
        public static PersonaDatabase Database { get; private set; }

        public App()
        {
            InitializeComponent();

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "personas.db3");
            Database = new PersonaDatabase(dbPath);

            MainPage = new NavigationPage(new MainPage());
        }
    }
}