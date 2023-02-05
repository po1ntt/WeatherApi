using InputKit.Handlers;
using UraniumUI;
using WeatherApp.Service;
using WeatherApp.Views;

namespace WeatherApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseUraniumUIMaterial()
                .UseUraniumUI()
                .ConfigureMauiHandlers(handlers =>{
                    handlers.AddInputKitHandlers();
                 })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFontAwesomeIconFonts();

                });
            builder.Services.AddSingleton<IRestDataService, RestDataService>();
            builder.Services.AddSingleton<HomePage>();

            return builder.Build();
        }
    }
}