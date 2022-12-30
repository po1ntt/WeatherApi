using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WeatherApi.Model;

namespace WeatherApi.Service
{
    public class RepeatService : BackgroundService
    {
        private IServiceScopeFactory services { get; set; }
        public RepeatService(IServiceScopeFactory service)
        {
            services = service;
        }
        private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(1));
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {

                await UpdateWeatherInTowns();
            }


        }
        [HttpPost("updateWeather")]
        private async Task UpdateWeatherInTowns()
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                HttpResponseMessage resultweather = null;
                var towns = db.Town.ToList();
                foreach (var town in towns)
                {
                    HttpClient client = new HttpClient();

                    try
                    {
                        resultweather = client.GetAsync(string.Format($"http://api.open-meteo.com/v1/forecast?latitude={Math.Round(town.latitude, 0)}&longitude={Math.Round(town.longitude, 0)}&hourly=temperature_2m&past_days=7")).Result;
                        resultweather.EnsureSuccessStatusCode();

                    }
                    catch (Exception text)
                    {
                        Console.WriteLine(text.Message);
                    }
                    finally
                    {
                        if (resultweather != null)
                        {
                            string responsebodyweather = await resultweather.Content.ReadAsStringAsync();
                            WeatherToScratch? dataweather = JsonConvert.DeserializeObject<WeatherToScratch>(responsebodyweather);
                            var weather = db.Weather.FirstOrDefault(p => p.Towns.id_town == town.id_town);
                            weather.time = string.Join(' ', dataweather.hourly.time);
                            weather.temperatyre_2m = string.Join(' ', dataweather.hourly.temperature_2m);
                            weather.UpdateDate = DateTime.Now;
                            await db.SaveChangesAsync();
                            Console.WriteLine($"Погода города с названием - {town.name} изменена");
                        }
                    }
                }
                Console.WriteLine("Обновление температуры города прошло успешно");

            }
        }
    }
}
