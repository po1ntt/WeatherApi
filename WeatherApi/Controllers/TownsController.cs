using Microsoft.AspNetCore.Mvc;
using WeatherApi.Model;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text.Json;

using static System.Net.WebRequestMethods;
using static WeatherApi.Model.WeatherInfo;
using System.Runtime.Serialization;
using System.Globalization;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TownsController : Controller
    {
        ApplicationDbContext db;
        public TownsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [HttpGet(Name = "GetTownInfoByName")]
        public async Task<Root>? Get(string nametown)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync(string.Format($"http://api.positionstack.com/v1/forward?access_key=919d39c1f3433190510852eb038055cb&query={nametown}&limit=1")).Result;
            result.EnsureSuccessStatusCode();
            if (result != null)
            {
                string responsebody = await result.Content.ReadAsStringAsync();
                Root? data = JsonConvert.DeserializeObject<Root>(responsebody);
             
             
                return data;

            }
            else
            {
                return null;
            }
           

        }
        [HttpGet("GetWeaherTown")]
        public async Task<WeatherToScratch>? GetWeather(string nametown)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync(string.Format($"http://api.positionstack.com/v1/forward?access_key=919d39c1f3433190510852eb038055cb&query={nametown}&limit=1")).Result;
            result.EnsureSuccessStatusCode();
            if (result != null)
            {
                string responsebody = await result.Content.ReadAsStringAsync();
                Root? data = JsonConvert.DeserializeObject<Root>(responsebody);
                double lon = 0;
                double lan = 0;
                foreach(var item in data.data)
                {
                    lon = Math.Round(item.longitude, 0);
                    lan = Math.Round(item.latitude,0);
                    
                    
                }

                var resultweather = client.GetAsync(string.Format($"http://api.open-meteo.com/v1/forecast?latitude={lan}&longitude={lon}&hourly=temperature_2m&past_days=7")).Result;
                resultweather.EnsureSuccessStatusCode();
                if(resultweather!= null)
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
            else
            {
                return null;
            }


        }
        [HttpGet("GetWetherInInterval")]
        public async Task<WeatherToScratch>? GetWeatherInInterval(DateTime date1, DateTime date2)
        {
            HttpClient client = new HttpClient();
            string dateFirst = date1.ToString(format:("yyyy-MM-dd"), DateTimeFormatInfo.InvariantInfo);
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
        [HttpPost(Name = "Post")]
        public async Task<string> Post(string nametown)
        {
            HttpClient client = new HttpClient();
            var result = client.GetAsync(string.Format($"http://api.positionstack.com/v1/forward?access_key=919d39c1f3433190510852eb038055cb&query={nametown}&limit=1")).Result;
            result.EnsureSuccessStatusCode();
            if (result != null)
            {
                string responsebody = await result.Content.ReadAsStringAsync();
                Root? data = JsonConvert.DeserializeObject<Root>(responsebody);
                foreach(var item in data.data.ToList())
                {
                    db.Town.Add(new Towns
                    {
                        latitude = item.latitude,
                        name = item.name,
                        administrative_area = item.administrative_area,
                        confidence = item.confidence,
                        label = item.label,
                        longitude = item.longitude,
                        continent = item.continent,
                        country = item.country,
                        country_code = item.country_code

                    });
                   await db.SaveChangesAsync();
                }

                return "Город Успешно добавлен";

            }
            else
            {
                return null;
            }


        }

    }
}
