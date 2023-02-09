namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("@32302e342e30ViMCC384CXtWm518ori9x88jGttyPyivBGHNNKwubdk=");

            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}