using WeatherApp.Views;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {

            InitializeComponent();

            if (Preferences.Default.Get("id_user", 0) != 0)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new AuthorizePage();
            }
        }
    }
}