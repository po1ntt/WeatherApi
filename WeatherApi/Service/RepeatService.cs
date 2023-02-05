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
                var towns = db.town.ToList();
                foreach (var town in towns)
                {
                    HttpClient client = new HttpClient();

                    try
                    {
                        resultweather = client.GetAsync(string.Format($"http://api.open-meteo.com/v1/forecast?latitude={Math.Round(town.latitude, 0)}&longitude={Math.Round(town.longitude, 0)}&hourly=temperature_2m,relativehumidity_2m,precipitation&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset&windspeed_unit=ms&timezone=auto&past_days=7")).Result;
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
                            
                            var weather = db.weather.FirstOrDefault(p => p.Towns.id_town == town.id_town);
                            if(weather!= null)
                            {

                            
                            weather.time = string.Join(' ', dataweather.hourly.time);
                            weather.temperatyre2M = string.Join(' ', dataweather.hourly.temperature_2m);

                            weather.precipitation = string.Join(' ', dataweather.hourly.precipitation);
                            weather.sunrise = string.Join(' ', dataweather.daily.sunrise);
                            weather.sunset = string.Join(' ', dataweather.daily.sunset);
                            weather.relativehimidity_2m = string.Join(' ', dataweather.hourly.relativehumidity_2m);
                            weather.temperatyre_2m_max = string.Join(' ', dataweather.daily.temperature_2m_max);
                            weather.temperatyre_2m_min = string.Join(' ', dataweather.daily.temperature_2m_min);
                            weather.dateDay = string.Join(' ', dataweather.daily.dateDay);
                            weather.updateDate = DateTime.Now;
                            await db.SaveChangesAsync();
                            Console.WriteLine($"Погода города с названием - {town.name} изменена");
                           }
                        }
                    }
                }
                Console.WriteLine("Обновление температуры города прошло успешно");

            }
        }
    }
}
