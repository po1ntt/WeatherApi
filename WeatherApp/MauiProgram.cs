
using InputKit.Handlers;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using UraniumUI;
using WeatherApp.Service;
using WeatherApp.Views;
using LiveChartsCore.SkiaSharpView.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;

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
                .UseSkiaSharp(true)
                .UseMauiCompatibility()
                .ConfigureMauiHandlers(handlers =>{
                    handlers.AddInputKitHandlers();
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("20952.ttf", "Serif");

                    fonts.AddFontAwesomeIconFonts();

                });
            builder.Services.AddSingleton<IRestDataService,RestDataService>();
            return builder.Build();
        }
    }
}