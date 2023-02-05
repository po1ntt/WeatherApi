using WeatherApp.Views;

namespace WeatherApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("HomePage", typeof(HomePage));
            Routing.RegisterRoute("AuthorizePage", typeof(AuthorizePage));
            Routing.RegisterRoute("RegistrationPage", typeof(RegistrationPage));
            Routing.RegisterRoute("TownInfoPage", typeof(TownInfoPage));

        }
    }
}