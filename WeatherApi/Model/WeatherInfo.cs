using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApi.Model
{
    public class WeatherInfo
    {
        [Key]
        public int id_weather { get; set; }
     
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string? timezone { get; set; }
        public string? timezone_abbreviation { get; set; }
        public double elevation { get; set; }
        [JsonProperty("hourly")]
        public Hourly? hourly_ { get; set; }
        [JsonProperty("temperatyre_2m")]
        public HourlyUnits? temperatyre_2m_ { get; set; }

        public class Hourly
        {
            [Key]
            public int id_hourly { get; set; }
            public String? Time { get; set; }
            public String? Temperature_2m { get; set; }
            [NotMapped]
            public List<string>? time
            {
                set
                {
                    Time = String.Join(",", value);
                }
                get
                {
                    if (Time != null)
                    {
                        return Time.Split(',').ToList();

                    }
                    else
                    {
                        return null;
                    }                        

                }
                
            }
            [NotMapped]
            public List<double>? temperature_2m
            {
                set
                {
                    Temperature_2m = String.Join(",", value);
                }
                get
                {
                    if (Temperature_2m != null)
                    {
                        List<double>? doubles = null;
                        List<string> temperatyre = Temperature_2m.Split(',').ToList();
                        foreach (var item in temperature_2m)
                        {
                            doubles.Add(Convert.ToDouble(item));
                        }
                        return doubles;
                    }
                    else
                    {
                        return null;
                    }
                }
               
            }
        }
     
        public class HourlyUnits
        {
            [Key]
            public int id_hourlyunits { get; set; }

            public string time { get; set; }
            public string temperature_2m { get; set; }
        }

    }
}
