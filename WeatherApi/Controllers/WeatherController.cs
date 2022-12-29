using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using WeatherApi.Model;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController
    {
        ApplicationDbContext  db;
        public WeatherController(ApplicationDbContext db)
        {
            this.db = db;
        }
        #region getinfoweather
        [HttpGet("GetWeaherTown")]
        public async Task<WeatherToScratch>? GetWeather(Towns town)
        {
            HttpClient client = new HttpClient();
          
           
                var resultweather = client.GetAsync(string.Format($"http://api.open-meteo.com/v1/forecast?latitude={Math.Round(town.latitude, 0)}&longitude={Math.Round(town.longitude, 0)}&hourly=temperature_2m&past_days=7")).Result;
                resultweather.EnsureSuccessStatusCode();
                if (resultweather != null)
                {
                    string responsebodyweather = await resultweather.Content.ReadAsStringAsync();
                    WeatherToScratch? dataweather = JsonConvert.DeserializeObject<WeatherToScratch>(responsebodyweather);

                    return dataweather;
                }
                else
                {
                    return null;
                }
            
           



        }
        [HttpGet("GetWetherInInterval")]
        public async Task<WeatherToScratch>? GetWeatherInInterval(DateTime date1, DateTime date2)
        {
            HttpClient client = new HttpClient();
            string dateFirst = date1.ToString(format: ("yyyy-MM-dd"), DateTimeFormatInfo.InvariantInfo);
            string dateEnd = date2.ToString(format: ("yyyy-MM-dd"), DateTimeFormatInfo.InvariantInfo);

            var resultweather = client.GetAsync(string.Format($"http://api.open-meteo.com/v1/forecast?latitude=55&longitude=37&hourly=temperature_2m&start_date={dateFirst}&end_date={dateEnd}")).Result;

            resultweather.EnsureSuccessStatusCode();
            if (resultweather != null)
            {



                string responsebodyweather = await resultweather.Content.ReadAsStringAsync();
                WeatherToScratch? dataweather = JsonConvert.DeserializeObject<WeatherToScratch>(responsebodyweather);

                return dataweather;



            }
            else
            {
                return null;
            }

        }
        #endregion
        [HttpPost("WeatherByTown")]
        public async Task<string> addWeatherToTown(Towns townsr)
        {
            WeatherToScratch weatherToScratch = GetWeather(townsr).Result;
            db.Weather.Add(new WeatherInfo
            {
                Towns= townsr,
                time = string.Join(' ', weatherToScratch.hourly.time),
                temperatyre_2m = string.Join(' ', weatherToScratch.hourly.temperature_2m)
                
            });
            db.SaveChanges();
            return "Погода добавлена";
            
        }
        
        [HttpPost("TownLocalizationByName")]
        public async Task<Towns> TownLocalizationByName(string nametown)
        {
            HttpClient client = new HttpClient();
            string realnamecity = "";
            var result = client.GetAsync(string.Format($"http://api.positionstack.com/v1/forward?access_key=919d39c1f3433190510852eb038055cb&query={nametown}&limit=1")).Result;
            result.EnsureSuccessStatusCode();
            if (result != null)
            {
                string responsebody = await result.Content.ReadAsStringAsync();
                Root? data = JsonConvert.DeserializeObject<Root>(responsebody);
                foreach (var item in data.data.ToList())
                {
                    db.Town.Add(new Towns
                    {
                        latitude = item.latitude,
                        name = item.name,

                        longitude = item.longitude,
                        continent = item.continent,
                        country = item.country,
                        locality = item.locality,



                    });
                    db.SaveChanges();
                    realnamecity = item.name;

                }

                Towns town = db.Town.FirstOrDefault(p => p.name == realnamecity);
               
                    return town;

                

            }
            else
            {
                return null;
            }


        }
        [HttpPost("AddNewTown")]
        public async Task<string> AddNewTown(string nametown)
        {
            Towns? townsforsecuring = db.Town.FirstOrDefault(p => p.name.Contains(nametown));
            if (townsforsecuring != null)
            {
                await addWeatherToTown(townsforsecuring);
                return "Погода добавлена";

            }
            else
            {
                Towns towns = await TownLocalizationByName(nametown);
                await addWeatherToTown(towns);
                return "Это работает";

            }

        }
    }
}
