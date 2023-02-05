using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WeatherApi.Model
{
    public class WeatherToScratch
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }
        public HourlyScr hourly { get; set; }
        public Daily daily { get; set; }
        public class HourlyScr
        {
            public List<string>? time { get; set; }
            public List<double>? temperature_2m { get; set; }
            public List<int> relativehumidity_2m { get; set; }
            public List<double> precipitation { get; set; }
        }
        public class Daily
        {
            [JsonProperty("time")]
            public List<string> dateDay { get; set; }
            public List<double> temperature_2m_max { get; set; }
            public List<double> temperature_2m_min { get; set; }
            public List<string> sunrise { get; set; }
            public List<string> sunset { get; set; }
        }


    }
}
