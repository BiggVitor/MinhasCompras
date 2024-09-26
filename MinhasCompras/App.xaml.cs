using MinhasCompras.Helpers;

namespace MinhasCompras
{

    //Criando a classe app
    public partial class App : Application
    {
       
        static SQLiteDatabaseHelper database;


        public static SQLiteDatabaseHelper Database
        {
            get
            {
                //Verifica se o banco de dados
                if (database == null)
                {
                    //Definindo o caminho do banco de dados
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "arquivo.db3");
                    
                    database = new SQLiteDatabaseHelper(path);
                }
                //Retorna a instância do banco
                return database;
            }
        }

        public App()
        {
            InitializeComponent(); //Inicializa a aplicação

           
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            MainPage = new AppShell();
        }
    }
}
