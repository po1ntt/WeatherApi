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
        public HourlyUnitsScr hourly_units { get; set; }
        public HourlyScr hourly { get; set; }
        public class HourlyScr
        {
            public List<string> time { get; set; }
            public List<double> temperature_2m { get; set; }
        }

        public class HourlyUnitsScr
        {
            public string time { get; set; }
            public string temperature_2m { get; set; }
        }

      


    }
}
