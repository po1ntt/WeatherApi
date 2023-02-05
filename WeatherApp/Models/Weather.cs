using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class Weather
    {
        public int idWeather { get; set; }
        public int townsId { get; set; }
        public string dateDay { get; set; }
        public string time { get; set; }
        public string temperatyre2M { get; set; }
        public string temperatyre_2m_max { get; set; }
        public string temperatyre_2m_min { get; set; }
        public string relativehimidity_2m { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }

    }
}
