using Npgsql;

namespace LopushokApp
{
    internal static class Program
    {
        public static ApplicationContext Form0;

        static string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=25481;Database=LopushokDatabase";
        public static NpgsqlConnection con = new NpgsqlConnection(ConnectionString);
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form0 = new ApplicationContext(new ProductsForm(1));
            Application.Run(Form0);
        }

        public static void ChangeForm(Form oldForm, Form newForm)
        {
            Form0.MainForm = newForm;
            Form0.MainForm.Show();
            oldForm.Close();
        }
    }
}