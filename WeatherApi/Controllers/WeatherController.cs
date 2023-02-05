using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using WeatherApi.Model;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController
    {
        ApplicationDbContext db;
        public WeatherController(ApplicationDbContext db)
        {
            this.db = db;
        }


        #region getinfoweather
        [HttpGet("ReturnWeatherByTown")]
        public async Task<WeatherInfo>? ReturnWeatherByTown(int id_town)
        {
            var dataWeather = await db.weather.FirstAsync(p => p.Towns.id_town == id_town);
            if (dataWeather != null)
            {
                return dataWeather;
            }
            else
            {
                Debug.WriteLine("Погода по данному городу не найдена!");
                return null;
            }





        }
        [HttpGet("GetWeaherTown")]
        public async Task<WeatherToScratch>? GetWeather(Towns town)
        {
            HttpClient client = new HttpClient();


            var resultweather = client.GetAsync(string.Format($"http://api.open-meteo.com/v1/forecast?latitude={Math.Round(town.latitude, 0)}&longitude={Math.Round(town.longitude, 0)}&hourly=temperature_2m,relativehumidity_2m,precipitation&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset&windspeed_unit=ms&timezone=auto&past_days=7")).Result;
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
            db.weather.Add(new WeatherInfo
            {
                Towns = townsr,
                time = string.Join(' ', weatherToScratch.hourly.time),
                temperatyre2M = string.Join(' ', weatherToScratch.hourly.temperature_2m),
                updateDate = DateTime.Now
               

            });
            await db.SaveChangesAsync();
            return "Погода добавлена";

        }

        [HttpPost]
        [Route("addnewtown")]
        public async Task<Towns> AddNewTown([FromBody]string nametown)
        {
            Towns town = new Towns();
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
                    realnamecity = item.name;

                    var checkexiststown = db.weather.FirstOrDefault(p => p.Towns.name == realnamecity);
                    if (checkexiststown != null)
                    {
                        town = await db.town.FirstAsync(p => p.id_town == checkexiststown.townsId);
                        return town;
                    }
                    else
                    {

                        db.town.Add(new Towns
                        {
                            latitude = item.latitude,
                            name = item.name,

                            longitude = item.longitude,
                            continent = item.continent,
                            country = item.country,
                            locality = item.locality,



                        });
                        await db.SaveChangesAsync();
                        town =  await db.town.FirstAsync(p => p.name == realnamecity);
                        WeatherToScratch weatherToScratch = GetWeather(town).Result;
                        db.weather.Add(new WeatherInfo
                        {
                            Towns = town,
                            time = string.Join(' ', weatherToScratch.hourly.time),
                            temperatyre2M = string.Join(' ', weatherToScratch.hourly.temperature_2m),
                            precipitation = string.Join(' ', weatherToScratch.hourly.precipitation),
                            sunrise = string.Join(' ', weatherToScratch.daily.sunrise),
                            sunset = string.Join(' ',weatherToScratch.daily.sunset),
                            relativehimidity_2m = string.Join(' ', weatherToScratch.hourly.relativehumidity_2m),
                            temperatyre_2m_max = string.Join(' ', weatherToScratch.daily.temperature_2m_max),
                            temperatyre_2m_min = string.Join(' ', weatherToScratch.daily.temperature_2m_min),
                            dateDay = string.Join(' ', weatherToScratch.daily.dateDay),
                            updateDate = DateTime.Now


                        });
                        await db.SaveChangesAsync();
                        return town;
                    }

                }

            }
            return town;
           
        }
        [HttpGet("GetWeatherByTown")]
        public async Task<WeatherInfo> GetWeatherByTown(int id_town)
        {
            WeatherInfo? weather = await db.weather.FirstAsync(p => p.Towns.id_town == id_town);
            return weather;
        }
    }
}
