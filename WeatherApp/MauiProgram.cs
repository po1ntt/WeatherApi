
using InputKit.Handlers;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using UraniumUI;
using Syncfusion.Maui.Core.Hosting;
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
                .ConfigureSyncfusionCore()
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

            return builder.Build();
        }
    }
}