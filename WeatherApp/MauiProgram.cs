using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using InputKit.Handlers;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Controls.Handlers.Compatibility;
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
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseMauiCommunityToolkitCore()
                .UseMauiCompatibility()
                .ConfigureMauiHandlers(handlers =>{
                    handlers.AddInputKitHandlers();
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFontAwesomeIconFonts();

                });

            return builder.Build();
        }
    }
}